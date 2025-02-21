using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EduLearnAPI.Models
{
    public class Enrollment
    {
        [BsonId]
        public int Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("CourseId")]
        public int CourseId { get; set; }

        [BsonElement]
        public Course Course { get; set; }
    }
}
