using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class PreviewsCopyMapper : MongoMapper
    {
        public PreviewsCopyMapper(IOptions<Config> config)
            : base(config)
        { } 
        
        public async Task<PreviewsItem> SaveAsync(PreviewsCopy copy)
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
    }
}
