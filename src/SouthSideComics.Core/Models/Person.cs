using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Models
{
    public class Person
    {
        public ObjectId Id { get; set; }        
        public string FullName { get; set; }
        public bool Writer { get; set; }
        public bool Artist { get; set; }
        public bool CoverArtist { get; set; }
    }
}
