using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class PreviewsItemMapper : MongoMapper
    {
        public PreviewsItemMapper(Config config)
            : base(config)
        { }
        

        public async Task<PreviewsItem> Save(PreviewsItem item)
        {
            var collection = GetCollection<PreviewsItem>("PreviewsItem");         
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<PagedList<PreviewsItem>> FindAll()
        {
            var colelction = GetCollection<PreviewsItem>("PreviewsItem");
            var results = await colelction.Find(p => p != null).ToListAsync();
            return new PagedList<PreviewsItem>(results);
        }
    }
}
