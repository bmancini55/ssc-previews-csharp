namespace SouthSideComics.Core.Common
{
    public class Config
    {
        public string MySqlConnectionString { get; set; }

        public string MongoConnectionString { get; set; }

        public string MongoDatabase { get; set; }

        public string ElasticsearchUri { get; set; }
    }
}
