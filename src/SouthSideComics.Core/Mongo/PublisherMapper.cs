using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace SouthSideComics.Core.Mongo
{
    public class PublisherMapper : MongoMapper<Publisher>
    {
        public PublisherMapper(IOptions<Config> config) 
            : base(config, "publisher")
        { }

        public async Task<Publisher> FindByNameAsync(string name)
        {
            return await GetCollection()
                .Find(p => p.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}
