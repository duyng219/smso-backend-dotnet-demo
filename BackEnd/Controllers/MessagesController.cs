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
    public class MessagesController : ControllerBase
    {
        private readonly IRepository<MessageBox> repository;
        public MessagesController(IRepository<MessageBox> repository)
        {
            this.repository = repository;
        }
        //Lấy tất cả dữ liệu
        [HttpGet]
        public async Task<ActionResult<List<MessageBox>>> GetAll()
        {
            var messages = await repository.ListAll();
            return Ok(messages);
        }
        //Lấy dữ liệu theo id
        [HttpGet("{MessageId}")]
        [SwaggerOperation(
           Summary = "Get all Messages",
           Description = "Get  log and all post of log by MessageId",
           OperationId = "MessagesController.GetAllMessages",
           Tags = new[] { "MessageBox" })
       ]
        public async Task<ActionResult<MessageBox>> GetService(int MessageId)
        {
            var messages = await repository.Get(b => b.MessageId == MessageId);

            if (messages.Count > 0)
            {
                return Ok(messages[0]);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Messages",
           Description = "Insert a messages",
           OperationId = "MessagesController.CreateMessages",
           Tags = new[] { "MessageBox" })
       ]
        public async Task<ActionResult<MessageBox>> InsertItem(MessageBox item)
        {
            var messages = await repository.Insert(item);
            return messages;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Messages",
            Description = "Update a Messages",
            OperationId = "MessagesController.UpdateMessages",
            Tags = new[] { "MessageBox" })
        ]
        public async Task<ActionResult<MessageBox>> UpdateItem(MessageBox item)
        {

            var messages = await repository.Update(item);
            return messages;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a Messages",
           Description = "Delete a Messages",
           OperationId = "MessagesController.DeleteMessages",
           Tags = new[] { "MessageBox" })
       ]
        [HttpDelete("{MessageId}")]
        public async Task<ActionResult> Delete(int MessageId)
        {
            var messages = await repository.GetById(MessageId);
            if (messages == null)
            {
                return NotFound();
            }
            await repository.Delete(messages);
            return Ok();
        }
    }
}
