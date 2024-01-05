using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public class CategorySettingRepository : ICategorySettingRepository
    {
        private readonly IMongoCollection<Link> _linkCollection;

        public CategorySettingRepository(IOptions<DbConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _linkCollection = database.GetCollection<Link>("Links");
        }

        public async Task<LinkSettings> GetAllAsync(string linkId)
        {
            var link = await _linkCollection.Find(u => u.Id == linkId).FirstOrDefaultAsync().ConfigureAwait(false);
            return link?.Settings ?? new LinkSettings();
        }

        public async Task<LinkSettings> GetByIdAsync(string linkId)
        {
            try
            {
                var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
                var projection = Builders<Link>.Projection.Include(u => u.Settings);

                var result = await _linkCollection.Find(filter).Project<Link>(projection).FirstOrDefaultAsync().ConfigureAwait(false);

                return result?.Settings;
            }
            catch (Exception ex)
            {
                // Hata işlemleri
                return null;
            }
        }

        public async Task<LinkSettings> CreateAsync(string linkId, LinkSettings linkSettings)
        {
            try
            {
                var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
                var update = Builders<Link>.Update.Set(u => u.Settings, linkSettings);

                var options = new UpdateOptions { IsUpsert = true };

                await _linkCollection.UpdateOneAsync(filter, update, options).ConfigureAwait(false);
                return linkSettings;
            }
            catch (Exception ex)
            {
                // Hata işlemleri
                return null;
            }
        }

        public async Task UpdateAsync(string linkId, LinkSettings linkSettings)
        {
            var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
            var update = Builders<Link>.Update.Set(u => u.Settings, linkSettings);

            await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }

        public async Task DeleteAsync(string linkId)
        {
            var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
            var update = Builders<Link>.Update.Set(u => u.Settings, null);

            await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }

    }
}
