using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public Task<List<Link>> GetAllAsync()
        {
            return _linkRepository.GetAllAsync();
        }

        public Task<Link> GetByIdAsync(string id)
        {
            return _linkRepository.GetByIdAsync(id);
        }

        public bool GetByKeyAsync(string key)
        {
            return _linkRepository.GetByKeyAsync(key);
        }

        public Task<Link> CreateAsync(Link link)
        {
            return _linkRepository.CreateAsync(link);
        }

        public Task UpdateAsync(string id, Link link)
        {
            return _linkRepository.UpdateAsync(id, link);
        }

        public Task DeleteAsync(string id)
        {
            return _linkRepository.DeleteAsync(id);
        }

    }
}
