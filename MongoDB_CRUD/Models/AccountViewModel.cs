using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDB_CRUD.Models
{
    public class AccountViewModel
    {
        public List<LoginViewModel> LoginModel { get; set; }
        public List<ResetPasswordViewModel> ResetPasswordModel { get; set; }
        public List<RegisterViewModel> RegisterModel { get; set; }
        public List<TokenViewModel> TokenModel { get; set; }

        public class LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
            public bool lockoutOnFailure { get; set; } = false;
        }

        public class ResetPasswordViewModel
        {
            public string Email { get; set; }
            public string NewPassword { get; set; }
        }

        public class RegisterViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class TokenViewModel
        {
            public string Token { get; set; }
        }
    }
}
