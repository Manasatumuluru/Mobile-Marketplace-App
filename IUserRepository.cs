using EduLearnAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EduLearnAPI.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        long GetUsersCount();
    }

    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(EduLearnDbServices dbServices)
        {
            _userCollection = dbServices.GetUserCollection();
        }

        public User GetUserById(int id)
        {
            return _userCollection.Find(user => user.Id == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _userCollection.Find(user => user.Email == email).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            _userCollection.InsertOne(user);
        }

        public void UpdateUser(User user)
        {
            _userCollection.ReplaceOne(existingUser => existingUser.Id == user.Id, user);
        }

        public void DeleteUser(int id)
        {
            _userCollection.DeleteOne(user => user.Id == id);
        }

        public long GetUsersCount()
        {
            return _userCollection.CountDocuments(FilterDefinition<User>.Empty);
        }
    }
}
