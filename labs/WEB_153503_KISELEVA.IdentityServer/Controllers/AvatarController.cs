using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WEB_153503_KISELEVA.IdentityServer.Models;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_153503_KISELEVA.IdentityServer.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class AvatarController : Controller
    {
        private IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;

        public AvatarController(IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _userManager = userManager;
        }

        // GET
        [HttpGet]
        public ActionResult Get()
        {
            var userId = _userManager.GetUserId(User);

            // Путь к папке с изображениями (Images)
            var imagesFolderPath = Path.Combine(_environment.ContentRootPath, "Images");

            // Путь к аватару пользователя
            var avatarPath = Path.Combine(imagesFolderPath, userId);
            avatarPath += ".jpg";

            Debug.WriteLine($"avatarPath: {avatarPath}");

            // Проверяем, существует ли файл аватара
            if (System.IO.File.Exists(avatarPath))
            {
                // Получаем MIME-тип файла аватара
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(avatarPath, out var contentType))
                {
                    contentType = "application/octet-stream"; // MIME-тип по умолчанию
                }

                // Возвращаем файл аватара
                var stream = new FileStream(avatarPath, FileMode.Open, FileAccess.Read);
                return File(stream, contentType);
            }
            else
            {
                // Файла аватара нет, возвращаем файл-заменитель
                var placeholderPath = Path.Combine(imagesFolderPath, "user_without_image.png");

                if (System.IO.File.Exists(placeholderPath))
                {
                    // Получаем MIME-тип файла-заменителя
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(placeholderPath, out var contentType))
                    {
                        contentType = "application/octet-stream"; // MIME-тип по умолчанию
                    }

                    // Возвращаем файл-заменитель
                    var stream = new FileStream(placeholderPath, FileMode.Open, FileAccess.Read);
                    return File(stream, contentType);
                }
                else
                {
                    // Файл-заменитель также отсутствует, возвращаем ошибку
                    return NotFound("Изображение не найдено.");
                }
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

