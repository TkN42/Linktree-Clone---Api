using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDB_CRUD.EskiModeller
{
    public class Base
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("isDelete")]
        public bool IsDelete { get; set; } = false;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [BsonElement("updatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [BsonElement("deletedDate")]
        public DateTime? DeletedDate { get; set; }


    }
}
