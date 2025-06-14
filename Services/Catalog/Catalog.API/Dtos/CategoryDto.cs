using Catalog.API.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
