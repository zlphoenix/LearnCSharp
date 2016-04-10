namespace TFSQuery
{
    public class TfsConfig
    {
        public TfsConfig()
        {
            TfsUrl = @"http://10.180.36.63:8080/tfs/GSP/";
            UserName = "administrator";
            Password = "Test1234";
            ProjectName = "GSP6.1";
        }

        public string TfsUrl { get; private set; } = "https://<HOST>/tfs/<COLLECTION>";
        public string Domain { get; private set; } = "<DOMAIN>";
        public string Password { get; private set; } = "<PASSWORD>";
        public string UserName { get; private set; } = "<USERNAME>";
        public string ProjectName { get; private set; } = "<PROJECT NAME>";
    }
}