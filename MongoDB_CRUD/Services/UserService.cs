using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;

namespace MongoDB_CRUD.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryService _categoryService;
        private readonly ILinkService _linkService;
        private readonly IGenerateUniqueKeyService _generateUniqueKeyService;

        public UserService(IUserRepository userRepository, ICategoryService categoryService, ILinkService linkService, IGenerateUniqueKeyService generateUniqueKeyService)
        {
            _userRepository = userRepository;
            _categoryService = categoryService;
            _linkService = linkService;
            _generateUniqueKeyService = generateUniqueKeyService;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public bool GetByKeyAsync(string key)
        {
            return _userRepository.GetByKeyAsync(key);
        }

        public Task<User> CreateAsync(User user)
        {
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

            if(user.Categories != null)
            {
                for (int i = 0; i < user.Categories.Count; i++)
                {
                    bool isKeyUnique = true;
                    string newKey = "";
                    while (isKeyUnique)
                    {
                        //newKey = GenerateUniqueKey();
                        newKey = _generateUniqueKeyService.GenerateUniqueKey();
                        isKeyUnique = GetByKeyAsync(newKey) || user.Categories.Any(c => c.Id == newKey);
                    }

                    user.Categories[i].Id = newKey;
                    var link = new Link();
                    link.Id = newKey;
                    //default bir atama yapılack mı ???
                    _linkService.CreateAsync(link).ConfigureAwait(false);
                }
            }

            return _userRepository.CreateAsync(user);
            //return _userRepository.CreateAsync(user);
        }

        public Task UpdateAsync(string id, User userIn, User user)
        {
            userIn.Categories = user.Categories;
            userIn.Id = user.Id;

            if (userIn.Name == null)
            {
                userIn.Name = user.Name;
            }
            if (userIn.Surname == null)
            {
                userIn.Surname = user.Surname;
            }
            if (userIn.Email == null)
            {
                userIn.Email = user.Email;
            }
            if (userIn.Password == null)
            {
                userIn.Password = user.Password;
            }
            if (userIn.HesapNo == null)
            {
                userIn.HesapNo = user.HesapNo;
            }

            return _userRepository.UpdateAsync(id, user);

            //return _userRepository.UpdateAsync(id, user);
        }

        public Task DeleteAsync(string id)
        {
            return _userRepository.DeleteAsync(id);
        }

    }
}
