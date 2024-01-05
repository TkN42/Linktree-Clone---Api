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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Link> _linkCollection;
        private readonly IMongoCollection<User> _userCollection;
        private readonly DbConfiguration _settings;

        public CategoryRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            //var client = new MongoClient(settings.Value.ConnectionString);
            //var database = client.GetDatabase(settings.Value.DatabaseName);
            _linkCollection = database.GetCollection<Link>("Links");
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<List<Category>> GetAllAsync(string userId)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync().ConfigureAwait(false);
            return user?.Categories ?? new List<Category>();
        }

        public async Task<Category> GetByIdAsync(string userId, string linkId)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, userId) & Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == linkId);
                var projection = Builders<User>.Projection.Include(u => u.Categories[-1]);

                var result = await _userCollection.Find(filter).Project<User>(projection).FirstOrDefaultAsync().ConfigureAwait(false);

                var category = result?.Categories?.LastOrDefault();

                return category;


            }
            catch (Exception ex) 
            {
                return null;
            }
        }

        public async Task<Category> CreateAsync(string userId, Category category)
        {
            try
            {
                var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    // Kullanıcı yoksa uygun bir hata mesajı döndürün veya kullanıcı oluşturun.
                    throw new InvalidOperationException("Kullanıcı bulunamadı.");
                }
                else
                {
                    // 'Categories' alanını kontrol et ve boş bir diziyle başlat gerekirse.
                    if (user.Categories == null)
                    {
                        user.Categories = new List<Category>();
                    }

                    // 'Categories' alanına yeni kategori ekleyin.
                    user.Categories.Add(category);

                    // Kullanıcıyı güncelleyin.
                    var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
                    var update = Builders<User>.Update.Set(u => u.Categories, user.Categories);

                    await _userCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
                }

                return category;
            }
            catch (Exception ex)
            {
                // Hata durumunu uygun şekilde ele alın.
                return null;
            }
        }

        public async Task UpdateAsync(string userId, string linkId, Category category)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId) & Builders<User>.Filter.ElemMatch(u => u.Categories, c => c.Id == linkId);
            var update = Builders<User>.Update.Set(c => c.Categories[-1], category);

            await _userCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }


        public async Task DeleteAsync(string userId, string linkId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.PullFilter(u => u.Categories, c => c.Id == linkId);

            await _userCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
            await _linkCollection.DeleteOneAsync(c => c.Id == linkId);
        }
    }
}
