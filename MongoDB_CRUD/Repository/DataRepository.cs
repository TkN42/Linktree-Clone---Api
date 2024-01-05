using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly IMongoCollection<LinkData> _collection;
        private readonly DbConfiguration _settings;
        public DataRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<LinkData>("LinkDatas");
        }

        public Task<List<LinkData>> GetAllAsync()
        {
            return _collection.Find(c => true).ToListAsync();
        }

        public Task<LinkData> GetByIdAsync(string id)
        {
            return _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<LinkData> CreateAsync(LinkData linkData)
        {
            await _collection.InsertOneAsync(linkData).ConfigureAwait(false);
            return linkData;
        }

        public Task UpdateAsync(string id, LinkData linkData)
        {
            return _collection.ReplaceOneAsync(c => c.Id == id, linkData);
        }

        public Task DeleteAsync(string id)
        {
            return _collection.DeleteOneAsync(c => c.Id == id);
        }

    }
}
