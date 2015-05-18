using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SouthSideComics.Core.Models
{
    public class Publisher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
