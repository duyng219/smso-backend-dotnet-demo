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
    public class PaymentHistoryController : ControllerBase
    {
        private readonly IRepository<PaymentHistory> repository;
        public PaymentHistoryController(IRepository<PaymentHistory> repository)
        {
            this.repository = repository;
        }
        //Lấy tất cả dữ liệu
        [HttpGet]
        public async Task<ActionResult<List<PaymentHistory>>> GetAll()
        {
            var payments = await repository.ListAll();
            return Ok(payments);
        }
        //Lấy dữ liệu theo id
        [HttpGet("{PaymentId}")]
        [SwaggerOperation(
           Summary = "Get all Payment",
           Description = "Get  log and all post of log by Payment",
           OperationId = "PaymentHistoryController.GetAllPayment",
           Tags = new[] { "PaymentHistory" })
       ]
        public async Task<ActionResult<PaymentHistory>> GetPayment(int PaymentId)
        {
            var payments = await repository.Get(b => b.PaymentId == PaymentId);

            if (payments.Count > 0)
            {
                return Ok(payments[0]);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Payment",
           Description = "Insert a Payment",
           OperationId = "PaymentHistoryController.CreatePayment",
           Tags = new[] { "PaymentHistory" })
       ]
        public async Task<ActionResult<PaymentHistory>> InsertItem(PaymentHistory item)
        {
            var payments = await repository.Insert(item);
            return payments;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Payment",
            Description = "Update a Payment",
            OperationId = "PaymentHistoryController.UpdatePayment",
            Tags = new[] { "PaymentHistory" })
        ]
        public async Task<ActionResult<PaymentHistory>> UpdateItem(PaymentHistory item)
        {

            var payments = await repository.Update(item);
            return payments;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a Payment",
           Description = "Delete a Payment",
           OperationId = "PaymentHistoryController.DeletePayment",
           Tags = new[] { "PaymentHistory" })
       ]
        [HttpDelete("{PaymentId}")]
        public async Task<ActionResult> Delete(int PaymentId)
        {
            var payments = await repository.GetById(PaymentId);
            if (payments == null)
            {
                return NotFound();
            }
            await repository.Delete(payments);
            return Ok();
        }
    }
}
