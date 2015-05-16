using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class GenreMapper : MongoMapper<Genre>
    {
        public GenreMapper(IOptions<Config> config) 
            : base(config, "genre")
        { }        
    }
}
