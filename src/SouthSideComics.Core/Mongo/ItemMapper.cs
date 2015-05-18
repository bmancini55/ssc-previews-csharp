using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class ItemMapper : MongoMapper<Item>
    {
        public ItemMapper(IOptions<Config> config) 
            : base(config, "item")
        { }       

        public async Task<PagedList<Item>> FindAsync(int page, int pagesize, string preview, string publisherId, string writerId, string artistId, Expression<Func<Item, object>> sorter)
        {
            FilterDefinitionBuilder<Item> builder = Builders<Item>.Filter;
            List<FilterDefinition<Item>> filters = new List<FilterDefinition<Item>>();
            FilterDefinition<Item> filter;

            if (!string.IsNullOrEmpty(preview))            
                filters.Add(builder.Regex("Previews.PreviewNumber", new BsonRegularExpression("^" + preview)));

            if (!string.IsNullOrEmpty(publisherId))
                filters.Add(builder.Eq(p => p.Publisher.Id, publisherId));

            if (!string.IsNullOrEmpty(writerId))
                filters.Add(builder.Eq(p => p.Writer.Id, writerId));

            if (!string.IsNullOrEmpty(artistId))
                filters.Add(builder.Eq(p => p.Artist.Id, artistId));

            if (filters.Count > 0)
                filter = builder.And(filters);
            else
                filter = new BsonDocument();

            var items = await GetCollection()
                .Find(filter)
                .SortBy(sorter)
                .Skip((page - 1) * pagesize)
                .Limit(pagesize)
                .ToListAsync();

            var count = await GetCollection()
                .Find(filter)
                .CountAsync();

            return new PagedList<Item>(items, page, pagesize, (int)count);
        }
    }
}
