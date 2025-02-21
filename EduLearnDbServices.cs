using EduLearnAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EduLearnAPI
{
    public class EduLearnDbServices
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMongoCollection<Enrollment> _enrollmentCollection;

        public EduLearnDbServices(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _userCollection = _database.GetCollection<User>(mongoDBSettings.Value.UserCollection);
            _courseCollection = _database.GetCollection<Course>(mongoDBSettings.Value.CourseCollection);
            _contactCollection = _database.GetCollection<Contact>(mongoDBSettings.Value.ContactCollection);
            _enrollmentCollection = _database.GetCollection<Enrollment>(mongoDBSettings.Value.EnrollmentCollection);
        }

        public IMongoCollection<User> GetUserCollection() => _userCollection;
        public IMongoCollection<Course> GetCourseCollection() => _courseCollection;
        public IMongoCollection<Contact> GetContactCollection() => _contactCollection;
        public IMongoCollection<Enrollment> GetEnrollmentCollection() => _enrollmentCollection;
    }
}
