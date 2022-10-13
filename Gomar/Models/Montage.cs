using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gomar.Models
{
    public class Montage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ImageName { get; set; }

        [BsonIgnore]
        public IFormFile ImageFile { get; set; }
    }
}
