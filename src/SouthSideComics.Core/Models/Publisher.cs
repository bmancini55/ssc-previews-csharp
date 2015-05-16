using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Models
{
    public class Publisher
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
    }
}
