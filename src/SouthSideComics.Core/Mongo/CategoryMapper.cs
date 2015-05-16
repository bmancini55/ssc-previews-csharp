using Microsoft.Framework.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Mongo
{
    public class CategoryMapper : MongoMapper<Category>
    {
        public CategoryMapper(IOptions<Config> config) : 
            base(config, "category")
        { }
    }
}
