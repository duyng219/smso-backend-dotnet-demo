using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IRepository<UserService> repository;
        public UserServiceController(IRepository<UserService> repository)
        {
            this.repository = repository;
        }
        //Lấy tất cả dữ liệu
        [HttpGet]
        public async Task<ActionResult<List<UserService>>> GetAll()
        {
            var userservice = await repository.ListAll();
            return Ok(userservice);
        }
        //Lấy dữ liệu theo id
        [HttpGet("{UserId}")]
        [SwaggerOperation(
           Summary = "Get all UserService",
           Description = "Get  log and all post of log by UserService",
           OperationId = "UserServiceController.GetAllUserService",
           Tags = new[] { "UserService" })
       ]
        public async Task<ActionResult<List<UserService>>> GetUserService(int UserId)
        {
            var userservice = await repository.Get(b => b.UserId == UserId);

            if (userservice.Count > 0)
            {
                return Ok(userservice);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a UserService",
           Description = "Insert a UserService",
           OperationId = "UserServiceController.CreateUserService",
           Tags = new[] { "UserService" })
       ]
        public async Task<ActionResult<UserService>> InsertItem(UserService item)
        {
            var userservice = await repository.Insert(item);
            return userservice;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a UserService",
            Description = "Update a UserService",
            OperationId = "UserServiceController.UpdateUserService",
            Tags = new[] { "UserService" })
        ]
        public async Task<ActionResult<UserService>> UpdateItem(UserService item)
        {

            var userservice = await repository.Update(item);
            return userservice;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a UserService",
           Description = "Delete a UserService",
           OperationId = "UserServiceController.DeleteUserService",
           Tags = new[] { "UserService" })
       ]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userservice = await repository.GetById(id);
            if (userservice == null)
            {
                return NotFound();
            }
            await repository.Delete(userservice);
            return Ok();
        }
    }
}
