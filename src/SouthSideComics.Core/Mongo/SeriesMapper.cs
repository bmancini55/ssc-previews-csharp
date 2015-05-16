using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class SeriesMapper : MongoMapper<Series>
    {
        public SeriesMapper(IOptions<Config> config) 
            : base(config, "series")
        { }                      

        public async Task<Series> FindByNameAsync(string name)
        {
            return await GetCollection()
                .Find(prop => prop.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}
