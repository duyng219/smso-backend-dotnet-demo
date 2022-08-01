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
    public class FriendsController : ControllerBase
    {
        private readonly IRepository<Friend> repository;
        public FriendsController(IRepository<Friend> repository)
        {
            this.repository = repository;
        }
        //Lấy tất cả dữ liệu
        [HttpGet]
        public async Task<ActionResult<List<Friend>>> GetAll()
        {
            var friends = await repository.ListAll();
            return Ok(friends);
        }
        //Lấy dữ liệu theo id
        [HttpGet("{UserId}")]
        [SwaggerOperation(
           Summary = "Get all Friend",
           Description = "Get  log and all post of log by Id",
           OperationId = "FriendsController.GetAllFriend",
           Tags = new[] { "Friend" })
       ]
        public async Task<ActionResult<List<Friend>>> GetService(int UserId)
        {
            var friends = await repository.Get(b => b.UserId == UserId);

            if (friends.Count > 0)
            {
                return Ok(friends);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Friend",
           Description = "Insert a Friend",
           OperationId = "FriendsController.CreateFriend",
           Tags = new[] { "Friend" })
       ]
        public async Task<ActionResult<Friend>> InsertItem(Friend item)
        {
            var friends = await repository.Insert(item);
            return friends;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Friend",
            Description = "Update a Friend",
            OperationId = "FriendsController.UpdateFriend",
            Tags = new[] { "Friend" })
        ]
        public async Task<ActionResult<Friend>> UpdateItem(Friend item)
        {

            var friends = await repository.Update(item);
            return friends;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a Friend",
           Description = "Delete a Friend",
           OperationId = "FriendsController.DeleteFriend",
           Tags = new[] { "Friend" })
       ]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var friends = await repository.GetById(Id);
            if (friends == null)
            {
                return NotFound();
            }
            await repository.Delete(friends);
            return Ok();
        }
    }
}
