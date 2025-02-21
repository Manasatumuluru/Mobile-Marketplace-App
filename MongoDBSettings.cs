namespace EduLearnAPI
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; }
        public string DatabaseName { get; set; }
        public string UserCollection { get; set; }
        public string CourseCollection { get; set; }
        public string ContactCollection { get; set; }
        public string EnrollmentCollection { get; set; }
    }
}