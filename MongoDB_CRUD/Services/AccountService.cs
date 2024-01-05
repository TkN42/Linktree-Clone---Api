using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MongoDB_CRUD.Models.AccountViewModel;

namespace MongoDB_CRUD.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly byte[] _key;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _tokenHandler = new JwtSecurityTokenHandler();
            _key = Encoding.ASCII.GetBytes("your-secret-key,your-secret-key,your-secret-key,your-secret-key,your-secret-key,your-secret-key,your-secret-key,your-secret-key");
        }

        public async Task<User> PasswordSignInAsync(LoginViewModel model)
        {
            return await _accountRepository.PasswordSignInAsync(model);
        }

        public async Task<bool> CreateAsync(RegisterViewModel model)
        {
            return await _accountRepository.CreateAsync(model);
        }

        public async Task<User> FindByNameAsync(ResetPasswordViewModel model)
        {
            return await _accountRepository.FindByNameAsync(model);
        }

        public async Task<bool> FindByEmailAsync(string email)
        {
            return await _accountRepository.FindByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User model)
        {
            return await _accountRepository.GeneratePasswordResetTokenAsync(model);
        }

        public async Task<bool> ResetPasswordAsync(User model, string token, string newPass)
        {
            return await _accountRepository.ResetPasswordAsync(model,token,newPass);
        }

        public async Task<string> GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.UserData, user.Id),
                    new Claim(ClaimTypes.Role, "user"), // Kullanıcının rolüne göre ayarla
                }),
                Expires = DateTime.UtcNow.AddHours(50), // Token süresi
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = _tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task<bool> ValidationTokenAsync(string tokenString)
        {
            try
            {
                    var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };

                
                var principal = await _tokenHandler.ValidateTokenAsync(tokenString, tokenValidationParameters);
                return principal.IsValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> LogoutAsync(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Token'ı doğrula
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                // Token'ın geçerlilik süresini kontrol et
                if (validatedToken.ValidTo > DateTime.UtcNow)
                {
                    // Token geçerli, ancak artık geçerli olmamalı
                    return false;
                }

                // Geçerli kullanıcı ID'sini al
                var userId = principal.FindFirst(ClaimTypes.UserData)?.Value;

                // Burada, userId kullanılarak ilgili kullanıcının token'ını geçersiz kılma işlemi gerçekleştirilmelidir.
                // Bu adım gerçek bir JWT servisi ile işbirliği yaparak gerçekleştirilmelidir.
                // Örnek: JWTService.InvalidateToken(userId);

                // Geçersiz kılma işlemi başarılı
                return true;
            }
            catch (Exception ex)
            {
                // Token doğrulama sırasında bir hata oluştu
                return false;
            }
        }
    }
}
