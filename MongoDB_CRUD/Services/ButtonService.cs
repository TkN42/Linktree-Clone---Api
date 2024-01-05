using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class ButtonService : IButtonService
    {
        private readonly IButtonRepository _buttonRepository;

        public ButtonService(IButtonRepository buttonRepository)
        {
            _buttonRepository = buttonRepository;
        }

        public Task<List<LinkButton>> GetAllAsync(string linkId)
        {
            return _buttonRepository.GetAllAsync(linkId);
        }

        public Task<LinkButton> GetByIdAsync(string linkId, string buttonId)
        {
            return _buttonRepository.GetByIdAsync(linkId, buttonId);
        }

        public Task<LinkButton> CreateAsync(string linkId, LinkButton linkButton)
        {
            return _buttonRepository.CreateAsync(linkId, linkButton);
        }

        public Task UpdateAsync(string linkId, string buttonId, LinkButton linkButton)
        {
            return _buttonRepository.UpdateAsync(linkId, buttonId, linkButton);
        }

        public Task DeleteAsync(string linkId, string buttonId)
        {
            return _buttonRepository.DeleteAsync(linkId, buttonId);
        }
    }
}
