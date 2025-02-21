using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EduLearnAPI.Models
{
    public class Course
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Caption")]
        public string Caption { get; set; }

    }
}
