using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

namespace MongoDB_CRUD.Models
{
    public class LinkData
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string Id { get; set; }

        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("categoryData")]
        public Dictionary<DateTime, int> CategoryData { get; set; }

        [BsonElement("linkButtonId")]
        public string LinkButtonId { get; set; }

        [BsonElement("linkButtonData")]
        public Dictionary<DateTime, int> LinkButtonData { get; set; }

        // datetime emin değilim.
    }
}
