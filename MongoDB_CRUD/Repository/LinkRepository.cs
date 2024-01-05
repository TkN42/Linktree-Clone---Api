using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public class LinkRepository : ILinkRepository
    {
        private readonly IMongoCollection<Link> _collection;
        private readonly DbConfiguration _settings;
        public LinkRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<Link>("Links");
        }

        public Task<List<Link>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }

        public Task<Link> GetByIdAsync(string id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public bool GetByKeyAsync(string key)
        {
            var filter = Builders<Link>.Filter.ElemMatch(u => u.Buttons, c => c.Id == key);
            return _collection.Find(filter).Any();
        }

        public async Task<Link> CreateAsync(Link link)
        {
            await _collection.InsertOneAsync(link).ConfigureAwait(false);
            return link;
        }

        public Task UpdateAsync(string id, Link link)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, link);
        }

        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }

    }
}
