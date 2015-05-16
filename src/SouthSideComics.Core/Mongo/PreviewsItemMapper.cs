using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class PreviewsItemMapper : MongoMapper<PreviewsItem>
    {
        public PreviewsItemMapper(IOptions<Config> config)
            : base(config, "previewsitem")
        { }                

        public async Task<PreviewsItem> SaveCopyAsync(PreviewsCopy copy)
        {            
            var filter = Builders<PreviewsItem>.Filter.Eq("DiamondNumber", copy.DiamondNumber);
            var update = Builders<PreviewsItem>.Update.Set("Copy", copy);
            var options = new FindOneAndUpdateOptions<PreviewsItem, PreviewsItem>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await GetCollection().FindOneAndUpdateAsync(filter, update, options);
            return result;
        }               

        public async Task<PagedList<PreviewsItem>> FindAsync(int page, int pagesize, string preview)
        {            
            var filter = Builders<PreviewsItem>.Filter.Regex(p => p.DiamondNumber, new BsonRegularExpression("^" + preview));            

            var results = await GetCollection()
                .Find(filter)
                .SortBy(p => p.DiamondNumber)
                .Skip((page - 1) * pagesize)
                .Limit(pagesize)
                .ToListAsync();

            var count = await GetCollection()
                .Find(filter)
                .CountAsync();

            return new PagedList<PreviewsItem>(results, page, pagesize, (int)count);
        }
    }
}
