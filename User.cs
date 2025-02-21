using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EduLearnAPI.Models
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("MobileNumber")]
        public string MobileNumber { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }
    }
}
