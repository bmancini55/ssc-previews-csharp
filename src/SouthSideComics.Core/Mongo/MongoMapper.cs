using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class MongoMapper
    {
        public MongoMapper(IOptions<Config> config)
        {
            this.ConnectionString = config.Options.MongoConnectionString;
            this.Database = config.Options.MongoDatabase;
        }

        public string ConnectionString { get; set; }

        public string Database { get; set; }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(Database);
            return db.GetCollection<T>(collectionName, new MongoCollectionSettings() { AssignIdOnInsert = true });
        }
    }
}
