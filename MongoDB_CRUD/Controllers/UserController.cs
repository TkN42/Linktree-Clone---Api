using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB_CRUD.Models;
using MongoDB_CRUD.Services;

namespace MongoDB_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        private readonly ILinkService _linkService;
        private readonly IGenerateUniqueKeyService _generateUniqueKeyService;
        public UserController(IUserService userService,ICategoryService categoryService, ILinkService linkService, IGenerateUniqueKeyService generateUniqueKeyService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _linkService = linkService;
            _generateUniqueKeyService = generateUniqueKeyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync().ConfigureAwait(false));
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userService.GetByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //for (int i = 0; i < user.Categories.Count; i++)
            //{
            //    bool isKeyUnique = true;
            //    string newKey = "";
            //    while (isKeyUnique)
            //    {
            //        //newKey = GenerateUniqueKey();
            //        newKey = _generateUniqueKeyService.GenerateUniqueKey();
            //        isKeyUnique = _userService.GetByKeyAsync(newKey) || user.Categories.Any(c => c.Id == newKey);
            //    }

            //    user.Categories[i].Id = newKey;
            //    var link = new Link();
            //    link.Id = newKey;
            //    //default bir atama yapılack mı ???
            //    await _linkService.CreateAsync(link).ConfigureAwait(false);
            //}

            await _userService.CreateAsync(user).ConfigureAwait(false);
            return Ok(user.Id);
        }

        // // Service yapıldı...
        //string GenerateUniqueKey()
        //{
        //    const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        //    const int keyLength = 8;
        //    char[] key = new char[keyLength];
        //    Random random = new Random();

        //    for (int i = 0; i < keyLength; i++)
        //    {
        //        key[i] = allowedChars[random.Next(0, allowedChars.Length)];
        //    }

        //    return new string(key);
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login(Login login)
        //{
        //    // Burada giriş işlemi yapılır. Kullanıcı adı ve şifre doğrulanır.
        //    // Eğer giriş başarılıysa, JWT token oluşturarak kullanıcıya gönderilebilir.
        //    // Eğer giriş başarısızsa hata mesajı dönebilirsiniz.
        //}

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User userIn)
        {
            //sadece user bilgileri 
            var user = await _userService.GetByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                //userIn.Categories = user.Categories;
                //userIn.Id = user.Id;

                //if (userIn.Name == null)
                //{
                //    userIn.Name = user.Name;
                //}
                //if (userIn.Surname == null)
                //{
                //    userIn.Surname = user.Surname;
                //}
                //if (userIn.Email == null)
                //{
                //    userIn.Email = user.Email;
                //}
                //if (userIn.Password == null)
                //{
                //    userIn.Password = user.Password;
                //}
                //if (userIn.HesapNo == null)
                //{
                //    userIn.HesapNo = user.HesapNo;
                //}

                await _userService.UpdateAsync(id, userIn, user).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex) 
            {
                var message = ex.Message;
                return BadRequest(message);
            }
        }

        //[HttpPut("{id:length(24)}/{category_id:length(8)}")]
        //public async Task<IActionResult> Update(string id,string category_id, User userIn)
        //{
        //    //sadece user bilgileri ve bir category
        //    var user = await _userService.GetByIdAsync(id).ConfigureAwait(false);
        //    var category = await _categoryService.GetByIdAsync(id,category_id).ConfigureAwait(false);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    try
        //    {
        //        for (int i = 0; i < userIn.Categories.Count; i++)
        //        {
        //            if (userIn.Categories[i].Id == category_id)
        //            {
        //                // burası işleri karıştır gibi...
        //                userIn.Categories[i] = category;
        //            }
        //        }
        //        userIn.Id = user.Id;
        //        await _userService.UpdateAsync(id, userIn).ConfigureAwait(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //    }
        //    return NoContent();
        //}

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetByIdAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(user.Id).ConfigureAwait(false);
            return NoContent();
        }

    }
}
