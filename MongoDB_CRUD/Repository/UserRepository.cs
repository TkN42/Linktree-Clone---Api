using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        private readonly DbConfiguration _settings;
        public UserRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                var list = await _collection.Find(c => true).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public Task<User> GetByIdAsync(string id)
        {
            var user = _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public bool GetByKeyAsync(string key)
        {
            var filter = Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == key);
            return _collection.Find(filter).Any();
        }

        public async Task<User> CreateAsync(User user)
        {
            await _collection.InsertOneAsync(user).ConfigureAwait(false);
            return user;
        }

        public Task UpdateAsync(string id, User user)
        {
            try
            {
                return _collection.ReplaceOneAsync(c => c.Id == id, user);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }

    }
}
