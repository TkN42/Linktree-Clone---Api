using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task<List<LinkData>> GetAllAsync()
        {
            return _dataRepository.GetAllAsync();
        }

        public Task<LinkData> GetByIdAsync(string id)
        {
            return _dataRepository.GetByIdAsync(id);
        }

        public Task<LinkData> CreateAsync(LinkData linkData)
        {
            return _dataRepository.CreateAsync(linkData);
        }

        public Task UpdateAsync(string id, LinkData linkData)
        {
            return _dataRepository.UpdateAsync(id, linkData);
        }

        public Task DeleteAsync(string id)
        {
            return _dataRepository.DeleteAsync(id);
        }

    }
}
