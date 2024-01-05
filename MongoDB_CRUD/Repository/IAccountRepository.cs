using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using static MongoDB_CRUD.Models.AccountViewModel;

namespace MongoDB_CRUD.Repository
{
    public interface IAccountRepository
    {
        //modeller ve dönüşler değişebilir ...
        Task<User> PasswordSignInAsync(LoginViewModel model);
        Task<bool> CreateAsync(RegisterViewModel model);
        Task<User> FindByNameAsync(ResetPasswordViewModel model);
        Task<bool> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(User model);
        Task<bool> ResetPasswordAsync(User model, string token, string newPass);
    }
}
