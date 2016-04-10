using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
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
            var fromDate = DateTime.Now.AddDays(-100);
            var toDate = DateTime.Now;

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
        }

        private static string GetGroupKey(TfvcChange c)
        {
            //Console.WriteLine("{0}", c.ChangeType);

            return c.Item.Path;
        }
    }
}
