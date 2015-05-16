using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public abstract class MongoMapper<T>
    {
        public MongoMapper(IOptions<Config> config, string collectionName)
        {
            ConnectionString = config.Options.MongoConnectionString;
            Database = config.Options.MongoDatabase;
            CollectionName = collectionName;                        
        }

        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string CollectionName { get; set; }        

        public virtual IMongoCollection<T> GetCollection()
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(Database);
            return db.GetCollection<T>(CollectionName);
        }

        public async Task<T> InsertAsync(T instance)
        {
            var collection = GetCollection();
            await collection.InsertOneAsync(instance);
            return instance;
        }

        public async Task<T> FindByIdAsync(Expression<Func<T, bool>> idFilter)
        {
            return await GetCollection()
                .Find(idFilter)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<T>> FindAllAsync(Expression<Func<T, object>> sorter = null)
        {
            var filter = new BsonDocument();
            var result = await GetCollection()
                .Find(filter)
                .SortBy(sorter)
                .ToListAsync();
            return new PagedList<T>(result);
        }

        public async Task<PagedList<T>> FindAllAsync(int page, int pagesize, Expression<Func<T, object>> sorter = null)
        {            
            var filter = new BsonDocument();
            var results = await GetCollection()
                .Find(filter)
                .SortBy(sorter)
                .Skip((page - 1) * pagesize)
                .Limit(pagesize)
                .ToListAsync();

            var count = await GetCollection()
                .Find(filter)
                .CountAsync();

            return new PagedList<T>(results, page, pagesize, (int)count);
        }                
    }
}
