using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB_CRUD.EskiModeller;

namespace MongoDB_CRUD.Models
{
    public class LinkTkN : Base
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }

        ////[BsonElement("icon")]
        ////public string Icon { get; set; }

        [BsonElement("groupName")]
        public string GroupName { get; set; }

        [BsonElement("linkName")]
        public string LinkName { get; set; }

        [BsonElement("linkAdress")]
        public string LinkAdress { get; set; }

        [BsonElement("linkShortUrl")]
        public string LinkShortUrl { get; set; }

        //[BsonElement("isActive")]
        //public bool IsActive { get; set; }

        ////[BsonElement("linkCount")]
        ////public string Count { get; set; }

    }
}
