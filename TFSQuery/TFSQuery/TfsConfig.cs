using System.Configuration;

namespace TFSQuery
{
    public class TfsConfig
    {
        public TfsConfig()
        {
            TfsUrl = ConfigurationManager.AppSettings["TfsUrl"];//@"http://10.180.36.90:8080/tfs/GSP/";
            UserName = ConfigurationManager.AppSettings["UserName"];//"administrator";
            Password = ConfigurationManager.AppSettings["Password"];// "Test1234";
            ProjectName = ConfigurationManager.AppSettings["ProjectName"];// "GSP6";
            Domain = ConfigurationManager.AppSettings["Domain"];//"TFSUPDATE";
        }

        public string TfsUrl { get; internal set; } = "https://<HOST>/tfs/<COLLECTION>";
        public string Domain { get; internal set; } = "<DOMAIN>";
        public string Password { get; internal set; } = "<PASSWORD>";
        public string UserName { get; internal set; } = "<USERNAME>";
        public string ProjectName { get; internal set; } = "<PROJECT NAME>";
    }
}