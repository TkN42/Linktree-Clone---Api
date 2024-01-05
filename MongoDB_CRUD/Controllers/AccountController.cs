using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MongoDB_CRUD.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB_CRUD.Services;
using static MongoDB_CRUD.Models.AccountViewModel;
using System;

namespace MongoDB_CRUD.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _accountService.PasswordSignInAsync(model);

                if (result != null)
                {
                    var token = await _accountService.GenerateToken(result);
                    return Ok(new { Token = token , userid = result.Id });
                }
                else
                {
                    return BadRequest("Geçersiz giriş denemesi");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return BadRequest("Geçersiz giriş denemesi");
                //throw;
            }
            
        }

        [HttpPost]
        [Route("ValidateToken")]
        public async Task<IActionResult> ValidateToken(TokenViewModel tokenModel)
        {
            var isValid = await _accountService.ValidationTokenAsync(tokenModel.Token);

            if (isValid)
            {
                return Ok("Token is valid");
            }
            else
            {
                return BadRequest("Token is not valid");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                User user = new User
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password
                };

                var createdUser = await _userService.CreateAsync(user);

                if (createdUser != null)
                {
                    return Ok("User created");
                }
                else
                {
                    // Eğer kullanıcı oluşturulamazsa, BadRequest ile bir hata mesajı döndürülebilir.
                    return BadRequest("User creation failed");
                }
            }
            catch (Exception ex)
            {
                // Eğer bir hata olursa, loglama veya başka bir işlem yapabilirsiniz.
                // Ayrıca, gerçek hata mesajını kullanıcıya göstermek uygun olmayabilir.
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                var user = await _accountService.FindByNameAsync(model);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

                var userIn = user;
                userIn.Password = model.NewPassword;
                var result = _userService.UpdateAsync(user.Id, userIn, user);
                //var resetToken = await _accountService.GeneratePasswordResetTokenAsync(user);
                //var reset = await _accountService.ResetPasswordAsync(user, resetToken, model.NewPassword);

                return Ok("Password reset successful");
            }
            catch (Exception)
            {
                return BadRequest("Password reset failed");
            }
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(string token)
        {
            try
            {
                // Çıkış işlemini gerçekleştirin. Bu, örneğin JWT token'ını geçersiz kılabilir veya oturum bilgisini silebilir.
                // Örnek olarak, JWT kullanıyorsanız, token'ı geçersiz kılabilirsiniz.
                bool logoutResult = await _accountService.LogoutAsync(token);

                if (logoutResult)
                {
                    // Başarılı çıkış durumunu döndürün.
                    return Ok("Logout successful");
                }
                else
                {
                    // Çıkış işlemi başarısız olduysa hata durumunu döndürün.
                    return BadRequest("Logout failed");
                }
            }
            catch (Exception ex)
            {
                // Çıkış işlemi sırasında bir hata oluştuysa, hata durumunu döndürün.
                return StatusCode(500, ex.Message);
            }
        }


    }
}
