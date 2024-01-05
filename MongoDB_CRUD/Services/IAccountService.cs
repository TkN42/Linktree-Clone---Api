using MongoDB_CRUD.Models;
using System.Threading.Tasks;
using static MongoDB_CRUD.Models.AccountViewModel;

namespace MongoDB_CRUD.Services
{
    public interface IAccountService
    {
        //modeller ve dönüşler değişebilir ...
        Task<User> PasswordSignInAsync(LoginViewModel model);
        Task<bool> CreateAsync(RegisterViewModel model);
        Task<User> FindByNameAsync(ResetPasswordViewModel model);
        Task<bool> FindByEmailAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(User model);
        Task<bool> ResetPasswordAsync(User model , string token , string newPass);
        Task<string> GenerateToken(User user);
        Task<bool> ValidationTokenAsync(string tokenString);
        Task<bool> LogoutAsync(string token);

    }
}
