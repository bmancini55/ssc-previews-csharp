using Microsoft.Framework.OptionsModel;
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

        public async Task<PagedList<PreviewsItem>> FindAllAsync()
        {
            var colelction = GetCollection<PreviewsItem>("PreviewsItem");
            var results = await colelction.Find(p => p != null).ToListAsync();
            return new PagedList<PreviewsItem>(results);
        }
    }
}
