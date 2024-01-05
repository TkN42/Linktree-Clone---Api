using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_CRUD.Models;
using static MongoDB_CRUD.Models.AccountViewModel;

namespace MongoDB_CRUD.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoCollection<User> _collection;
        private readonly DbConfiguration _settings;
        public AccountRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _collection = database.GetCollection<User>("Users");
        }

        public async Task<User> PasswordSignInAsync(LoginViewModel model)
        {
            var user = await _collection.Find(c => c.Email == model.Email && c.Password == model.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> CreateAsync(RegisterViewModel model)
        {
            return true;
        }

        public async Task<User> FindByNameAsync(ResetPasswordViewModel model)
        {
            User user = null;
            return user;
        }
        public async Task<bool> FindByEmailAsync(string email)
        {
            var user = await _collection.Find(c => c.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                return true;
            }
            //return user;
            return false;
        }


        public async Task<string> GeneratePasswordResetTokenAsync(User model)
        {
            return "";
        }

        public async Task<bool> ResetPasswordAsync(User model, string token, string newPass)
        {
            return true;
        }

    }
}
