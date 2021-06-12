using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gomar.Models
{
    public enum Category
    {
        Okna, Drzwi, Bramy, Rolety
    }
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Name { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Description { get; set; }
        public string ImageName { get; set; }
        [BsonIgnore]
        public IFormFile ImageFile { get; set; }
        [BsonIgnore]
        public string ImageSrc { get; set; }
    }
}
