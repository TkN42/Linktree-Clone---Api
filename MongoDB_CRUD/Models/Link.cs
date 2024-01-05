using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDB_CRUD.Models
{
    public class Link
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("settings")]
        public LinkSettings Settings { get; set; }

        [BsonElement("buttons")]
        public List<LinkButton> Buttons { get; set; }
    }

    public class LinkSettings
    {
        [BsonElement("color")]
        public string Color { get; set; }

        [BsonElement("backgroundColor")]
        public string BackgroundColor { get; set; }

        // Diğer
    }

    public class LinkButton
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("icon")]
        public string Icon { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }

        // Diğer
    }
}
