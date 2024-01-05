using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDB_CRUD.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("surname")]
        public string Surname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("hesapNo")]
        public string HesapNo { get; set; }

        [BsonElement("categories")]
        public List<Category> Categories { get; set; }

    }

    public class Category
    {
        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }
    }

    public class CategoryRequest
    {

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("categoryName")]
        public string CategoryName { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }

        [BsonElement("backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}
