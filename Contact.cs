using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EduLearnAPI.Models
{
    public class Contact
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }
    }
}
