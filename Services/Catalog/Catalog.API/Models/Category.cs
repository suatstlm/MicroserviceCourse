using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
    }
}
