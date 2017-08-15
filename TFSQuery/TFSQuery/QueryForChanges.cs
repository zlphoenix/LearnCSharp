using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TFSQuery
{
    public class QueryForChanges
    {
        private readonly TfsConfig tfsConfig = new TfsConfig();

        public async Task GetTopChangedFiles()
        {
            //Criteria used to limit results
            var maxChangesToConsider = 1000;
            var maxResults = 10;
            var fromDate = DateTime.Now.AddDays(-10);
            var toDate = DateTime.Now;
            var maxResultsPerPath = 100;

            var fileExtensionToInclude = new List<string> { ".cs", ".js" };
            var extensionExclusions = new List<string> { ".csproj", ".json", ".css" };
            var fileExclusions = new List<string> { "AssemblyInfo.cs", "jquery-1.12.3.min.js", "config.js" };
            //var pathExclusions = new List<string> {
            //    "/subdirToForceExclude1/",
            //    "/subdirToForceExclude2/",
            //    "/subdirToForceExclude3/",
            //};

            using (var collection = new TfsTeamProjectCollection(new Uri(tfsConfig.TfsUrl),
                new NetworkCredential(userName: tfsConfig.UserName, password: tfsConfig.Password
                    , domain: tfsConfig.Domain
                    )))
            {
                collection.EnsureAuthenticated();


                //Set up date-range criteria for query
                var criteria = new TfvcChangesetSearchCriteria
                {
                    FromDate = fromDate.ToShortDateString(),
                    ToDate = toDate.ToShortDateString(),
                    //FromId = 48503,
                    //ToId = 66416,
                    //IncludeLinks = true,
                    //ItemPath = @"$/GSP6.1/Dev/"
                    ItemPath = @"$/GSP6/Dev/"
                };

                var tfvc = collection.GetService(typeof(VersionControlServer)) as VersionControlServer;

                //Get changesets
                //Note: maxcount set to maxvalue since impact to server is minimized by linq query below
                var changeSets = tfvc.QueryHistory(path: criteria.ItemPath, version: VersionSpec.Latest,
                    deletionId: 0, recursion: RecursionType.Full, user: null,
                    versionFrom: new DateVersionSpec(fromDate), versionTo: new DateVersionSpec(toDate),
                    maxCount: int.MaxValue, includeChanges: true,
                    includeDownloadInfo: false, slotMode: true)
                    as IEnumerable<Changeset>;

                Console.WriteLine("from{0} to {1} changeset count{2} from id:{3}to id:{4}", fromDate.ToShortDateString(),
                    toDate.ToShortDateString(), changeSets.Count(), changeSets.First().ChangesetId, changeSets.Last().ChangesetId);
                //Filter changes contained in changesets
                var changes = changeSets.SelectMany(a => a.Changes)
                .Where(a => a.ChangeType != ChangeType.Lock || a.ChangeType != ChangeType.Delete || a.ChangeType != ChangeType.Property)
                //.Where(e => !e.Item.ServerItem.ContainsAny(pathExclusionspathExclusions))
                .Where(e => e.Item.ServerItem.LastIndexOf('.') >= 0)
                .Where(e => !e.Item.ServerItem.Substring(e.Item.ServerItem.LastIndexOf('/') + 1).ContainsAny(fileExclusions))

                .Where(e => !e.Item.ServerItem.Substring(e.Item.ServerItem.LastIndexOf('.')).ContainsAny(extensionExclusions))
                .Where(e => e.Item.ServerItem.Substring(e.Item.ServerItem.LastIndexOf('.')).ContainsAny(fileExtensionToInclude))
                .GroupBy(g => g.Item.ServerItem)
                .Select(d => new { File = d.Key, Count = d.Count() })
                .OrderByDescending(o => o.Count)
                .Take(maxResultsPerPath)
                ;


                //Write top items for each path to the console
                Console.WriteLine(criteria.ItemPath); Console.WriteLine("->");
                foreach (var change in changes)
                {
                    Console.WriteLine("ChangeCount: {0} : File: {1}", change.Count, change.File);
                }
                Console.WriteLine(Environment.NewLine);

            }
        }

        private static string GetGroupKey(TfvcChange c)
        {
            //Console.WriteLine("{0}", c.ChangeType);

            return c.Item.Path;
        }
        /*
        //connection
        VssConnection connection = new VssConnection(new Uri(tfsConfig.TfsUrl),
            new VssCredentials(new WindowsCredential(new NetworkCredential(tfsConfig.UserName, tfsConfig.Password))));
        //Get tfvc client

        var tfvcClient = await connection.GetClientAsync<TfvcHttpClient>();


        //Set up date-range criteria for query
        var criteria = new TfvcChangesetSearchCriteria
        {
            FromDate = fromDate.ToShortDateString(),
            ToDate = toDate.ToShortDateString(),
            //FromId = 48503,
            //ToId = 66416,
            //IncludeLinks = true,
            ItemPath = @"$/GSP6.1/Dev/"
            //ItemPath = @"$/GSP6/Dev/GFI/GSF/src/CustomVariable/CustomVariableCore/CustomVariableManager.cs"
        };
        var changeSets = new List<TfvcChangesetRef>(10000);
        var recordCount = 0;
        while (true)
        {


            //get change sets
            var pagedChangeSet =
                await tfvcClient.GetChangesetsAsync(project: tfsConfig.ProjectName,
                        maxChangeCount: maxChangesToConsider,
                        includeDetails: true, includeWorkItems: false, skip: recordCount,
                        //includeSourceRename: true,
                        searchCriteria: criteria
                        );
            //tfvcClient.GetBatchedChangesetsAsync()

            if (pagedChangeSet.Count > 0)
            {
                changeSets.AddRange(pagedChangeSet);
                recordCount += pagedChangeSet.Count;
                Console.WriteLine("Queried {0} records", recordCount);
            }
            else
            {
                break;
            }
        }
        var subTotalChanges = new List<Tuple<string, string, int>>();

        if (changeSets.Any())
        {

            Console.WriteLine("Changeset ID from {0} to {1}", changeSets.First().ChangesetId, changeSets.Last().ChangesetId);
            Console.WriteLine("Changeset Date from {0} to {1}", changeSets.First().CreatedDate.ToShortDateString(), changeSets.Last().CreatedDate.ToShortDateString());
            Console.WriteLine("TotalChangeSetCount={0}", changeSets.Count);
            //Console.ReadLine();
            //get files associated with changesets and count the number of commits against the files.
            //note: a changeset may contain multiple changes to the same file, 
            //i.e. where multiple people committed changes to the same file and it was subsequently merged.

            Parallel.ForEach(changeSets, changeSet =>
           {
               var changes = tfvcClient.GetChangesetChangesAsync(changeSet.ChangesetId);

               subTotalChanges.AddRange(changes.Result.GroupBy(GetGroupKey)
                                           .Select(g => new Tuple<string, string, int>(g.Key, g.First().Item.Path, g.Count())));
           });

            // tally up all the files and provide a distilled result set, take only the max result specified.
            var results = (from subTotal in subTotalChanges
                           group subTotal by subTotal.Item1 into g
                           select new
                           {
                               Key = g.First().Item2,
                               ChangeCount = g.Sum(s => s.Item3)
                           })
                          .OrderByDescending(o => o.ChangeCount).Take(maxResults);

            foreach (var result in results)
            {
                Console.WriteLine("ChangeCount: {0} --- File: {1}", result.ChangeCount, result.Key);
            }
        }
        else
        {
            Console.WriteLine("No record...");
        }
        */
    }

    public static class StringExtensions
    {
        public static bool ContainsAny(this string source, List<string> lookFor)
        {
            if (!string.IsNullOrEmpty(source) && lookFor.Count > 0)
            {
                return lookFor.Any(source.Contains);
            }
            return false;
        }
    }
}
