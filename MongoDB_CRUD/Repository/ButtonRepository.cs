using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_CRUD.Models;

namespace MongoDB_CRUD.Repository
{
    public class ButtonRepository : IButtonRepository
    {
        private readonly IMongoCollection<Link> _linkCollection;

        public ButtonRepository(IOptions<DbConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _linkCollection = database.GetCollection<Link>("Links");
        }

        public async Task<List<LinkButton>> GetAllAsync(string linkId)
        {
            var link = await _linkCollection.Find(u => u.Id == linkId).FirstOrDefaultAsync().ConfigureAwait(false);
            return link?.Buttons ?? new List<LinkButton>();
        }

        public async Task<LinkButton> GetByIdAsync(string linkId, string buttonId)
        {
            try
            {
                var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId) & Builders<Link>.Filter.ElemMatch(u => u.Buttons, c => c.Id == buttonId);
                var projection = Builders<Link>.Projection.Include(u => u.Buttons[-1]);

                var result = await _linkCollection.Find(filter).Project<Link>(projection).FirstOrDefaultAsync().ConfigureAwait(false);

                var button = result?.Buttons?.LastOrDefault();

                return button;


            }
            catch (Exception ex)
            {
                return null;
            }
            //var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId) & Builders<Link>.Filter.Eq(u => u.Buttons.Any(c => c.Id == buttonId), true);
            //var projection = Builders<Link>.Projection.Include(u => u.Buttons[-1]);

            //var result = await _linkCollection.Find(filter).Project<LinkButton>(projection).FirstOrDefaultAsync().ConfigureAwait(false);
            //return result;
        }

        public async Task<LinkButton> CreateAsync(string linkId, LinkButton linkButton)
        {
            try
            {
                var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
                var link = await _linkCollection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);

                if (link != null)
                {
                    // "buttons" alanı varsa ve null değilse, push işlemi uygula; aksi takdirde yeni bir diziyle set et
                    var update = link.Buttons != null
                        ? Builders<Link>.Update.Push(u => u.Buttons, linkButton)
                        : Builders<Link>.Update.Set(u => u.Buttons, new List<LinkButton> { linkButton });

                    var options = new UpdateOptions { IsUpsert = true }; // Belge bulunamazsa ekle (upsert)

                    await _linkCollection.UpdateOneAsync(filter, update, options).ConfigureAwait(false);
                    return linkButton;
                }
                else
                {
                    // Belge bulunamadıysa yeni bir belge oluştur
                    var newLink = new Link
                    {
                        Id = linkId,
                        Buttons = new List<LinkButton> { linkButton }
                        // Diğer özellikleri de ekleyebilirsiniz
                    };

                    await _linkCollection.InsertOneAsync(newLink).ConfigureAwait(false);
                    return linkButton;
                }
            }
            catch (Exception ex)
            {
                // Hata işlemleri
                return null;
            }
        }



        public async Task UpdateAsync(string linkId, string buttonId, LinkButton linkButton)
        {
            var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId) & Builders<Link>.Filter.ElemMatch(u => u.Buttons, c => c.Id == buttonId);
            var update = Builders<Link>.Update.Set(c => c.Buttons[-1], linkButton);

            await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);

            //var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId) & Builders<Link>.Filter.Eq(u => u.Buttons.Any(c => c.Id == buttonId), true);
            //var update = Builders<Link>.Update.Set(u => u.Buttons[-1], linkButton);

            //await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }

        public async Task DeleteAsync(string linkId, string buttonId)
        {
            var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
            var update = Builders<Link>.Update.PullFilter(u => u.Buttons, c => c.Id == buttonId);

            await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
            //await _linkCollection.DeleteOneAsync(c => c.Id == linkId);

            //var filter = Builders<Link>.Filter.Eq(u => u.Id, linkId);
            //var update = Builders<Link>.Update.PullFilter(u => u.Buttons, Builders<LinkButton>.Filter.Eq(c => c.Id, buttonId));

            //await _linkCollection.UpdateOneAsync(filter, update).ConfigureAwait(false);
        }
    }
}
