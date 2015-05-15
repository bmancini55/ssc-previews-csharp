using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Common
{
    public class Config
    {
        public string MySqlConnectionString { get; set; }

        public string MongoConnectionString { get; set; }

        public string MongoDatabase { get; set; }
    }
}
