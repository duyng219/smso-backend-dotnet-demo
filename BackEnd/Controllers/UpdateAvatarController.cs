using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BackEnd.DTO.User;
using BackEnd.Models;
using BackEnd.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateAvatarController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<tbUser> repository;
        public UpdateAvatarController(IRepository<tbUser> repository, IWebHostEnvironment env)
        {
            this.repository = repository;
            _env = env;
        }

        [HttpPost]
        [Route("UpdateFlie")]
        [SwaggerOperation(
            Summary = "UpdateFlie",
            Description = "UploadFlie",
            OperationId = "UploadFlie",
            Tags = new[] { "tbUser" })]
        public async Task<ActionResult<HttpResponseMessage>> HandleAsync(List<IFormFile> files, [FromForm] string userJson)
        {
            try
            {
                // Config JSON 
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
                };
                // Convert JSON string sang Object
                var userRequest = JsonSerializer.Deserialize<UserRequest>(userJson, options);
                // Khoi tao mot product moi
                tbUser user = null;
                if (files.Count > 0)
                {
                    var formFile = files[0];
                    if (formFile.Length > 0)
                    {
                        // Luu Product xuong BD
                        user = new tbUser()
                        {
                            UserId = userRequest.UserId,
                            Username = userRequest.Username,
                            Password = userRequest.Password,
                            Email = userRequest.Email,
                            Gender = userRequest.Gender,
                            Phone = userRequest.Phone,
                            Birthday = userRequest.Birthday,
                            Address = userRequest.Address,
                            MaritalStatus = userRequest.MaritalStatus,
                            Hobbies = userRequest.Hobbies,
                            Education = userRequest.Education,
                            Work = userRequest.Work,
                            Company = userRequest.Company,
                            Position = userRequest.Position,
                            Avatar = userRequest.Avatar,
                            IsAdmin = userRequest.IsAdmin,
                            dateCreate = userRequest.dateCreate,
                            dateUpdate = userRequest.dateUpdate,
                        };
                        await repository.Update(user);
                        // Sau khi luu Product se co duoc Product Id
                        var filePath = Path.Combine(_env.ContentRootPath, "Images", user.UserId.ToString());
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        filePath = Path.Combine(filePath, formFile.FileName);

                        using var stream = new FileStream(filePath, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        // Cap nhat lai url cua san pham sau luu xong hinh anh
                        user.Avatar = "Images/" + user.UserId.ToString() + "/" + formFile.FileName;
                        await repository.Update(user);
                    }
                }
                else
                {
                    return BadRequest();
                }
                var response = new
                {
                    user.UserId,
                    user.Username,
                    user.Gender,
                    user.Phone,
                    user.Birthday,
                    user.Address,
                    user.MaritalStatus,
                    user.Hobbies,
                    user.Education,
                    user.Work,
                    user.Company,
                    user.Position,
                    user.Avatar,
                    user.Password,
                    user.IsAdmin,
                    user.dateUpdate,
                    user.dateCreate,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
