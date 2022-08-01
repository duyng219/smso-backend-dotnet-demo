using BackEnd.Data;
using BackEnd.DTO;
using BackEnd.DTO.User;
using BackEnd.Models;
using BackEnd.Repository;
using BackEnd.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<tbUser> repository;
        
        public UsersController(IRepository<tbUser> repository, IWebHostEnvironment env)
        {
            this.repository = repository;
            _env = env;
        }
        //Lấy tất cả
        [HttpGet]
        public async Task<ActionResult<List<tbUser>>> GetAll()
        {
            var user = await repository.ListAll();
            return Ok(user);
        }
        //Lấy theo Id
        [HttpGet("{UserId}")]
        public async Task<ActionResult<tbUser>> GetById(int UserId)
        {
            var user = await repository.GetById(UserId);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }
        //Tạo
        [HttpPost]
        public async Task<ActionResult<tbUser>> CreateUser(tbUser user)
        {
            var User = await repository.Insert(user);
            return Ok(User);
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a User",
            Description = "Update a User",
            OperationId = "UsersController.UpdateUser",
            Tags = new[] { "tbUser" })
        ]
        public async Task<ActionResult<tbUser>> UpdateItem(tbUser item)
        {

            var User = await repository.Update(item);
            return User;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a User",
           Description = "Delete a User",
           OperationId = "UsersController.DeleteUser",
           Tags = new[] { "tbUser" })
       ]
        [HttpDelete("{UserId}")]
        public async Task<ActionResult> Delete(int UserId)
        {
            var User = await repository.GetById(UserId);
            if (User == null)
            {
                return NotFound();
            }
            await repository.Delete(User);
            return Ok();
        }
        //Upload hinh
        //[Authorize]
        [HttpPost]
        [Route("UploadFlie")]
        [SwaggerOperation(
            Summary = "UploadFlie",
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
                            Username = userRequest.Username,
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
                            dateCreate = userRequest.dateCreate,
                            dateUpdate = userRequest.dateUpdate,
                        };
                        await repository.Insert(user);
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
                    user.Password,
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

