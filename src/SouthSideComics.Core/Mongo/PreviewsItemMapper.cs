using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class PreviewsItemMapper : MongoMapper
    {
        public PreviewsItemMapper(IOptions<Config> config)
            : base(config)
        { }
        

        public async Task<PreviewsItem> SaveAsync(PreviewsItem item)
        {            
            var collection = GetCollection<PreviewsItem>("PreviewsItem");            
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<PreviewsItem> SaveCopyAsync(PreviewsCopy copy)
        {
            var collection = GetCollection<PreviewsItem>("PreviewsItem");
            var filter = Builders<PreviewsItem>.Filter.Eq("DiamondNumber", copy.DiamondNumber);
            var update = Builders<PreviewsItem>.Update.Set("Copy", copy);

            var options = new FindOneAndUpdateOptions<PreviewsItem, PreviewsItem>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await collection.FindOneAndUpdateAsync(filter, update, options);

            return result;
        }

        public async Task<PagedList<PreviewsItem>> Find(int page, int pagesize)
        {
            var collection = GetCollection<PreviewsItem>("PreviewsItem");

            // Find all by applying blank filter
            var filter = new BsonDocument();
            var results = await collection
                .Find(filter)
                .SortBy(p => p.DiamondNumber)
                .Skip((page - 1) * pagesize)
                .Limit(pagesize)
                .ToListAsync();

            var count = await collection
                .Find(filter)
                .CountAsync();

            return new PagedList<PreviewsItem>(results, page, pagesize, (int)count);
        }        
    }
}
