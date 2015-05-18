using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SouthSideComics.Core.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }        
        public string FullName { get; set; }
        public bool Writer { get; set; }
        public bool Artist { get; set; }
        public bool CoverArtist { get; set; }
    }
}
