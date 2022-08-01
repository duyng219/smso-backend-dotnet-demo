using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IRepository<ServiceRegister> repository;
        public ServicesController(IRepository<ServiceRegister> repository)
        {
            this.repository = repository;
        }
        //Lấy tất cả dữ liệu
        [HttpGet]
        public async Task<ActionResult<List<ServiceRegister>>> GetAll()
        {
            var services = await repository.ListAll();
            return Ok(services);
        }
        //Lấy dữ liệu theo id
        [HttpGet("{serviceid}")]
        [SwaggerOperation(
           Summary = "Get all Service",
           Description = "Get  log and all post of log by Service Id",
           OperationId = "ServicesController.GetAllService",
           Tags = new[] { "ServiceRegister" })
       ]
        public async Task<ActionResult<ServiceRegister>> GetService(int serviceid)
        {
            var services = await repository.Get(b => b.ServiceId == serviceid);

            if (services.Count > 0)
            {
                return Ok(services[0]);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Service",
           Description = "Insert a Service",
           OperationId = "ServicesController.CreateService",
           Tags = new[] { "ServiceRegister" })
       ]
        public async Task<ActionResult<ServiceRegister>> InsertItem(ServiceRegister item)
        {
            var serviceRegister = await repository.Insert(item);
            return serviceRegister;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Service",
            Description = "Update a Service",
            OperationId = "ServicesController.UpdateService",
            Tags = new[] { "ServiceRegister" })
        ]
        public async Task<ActionResult<ServiceRegister>> UpdateItem(ServiceRegister item)
        {

            var serviceRegister = await repository.Update(item);
            return serviceRegister;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a Service",
           Description = "Delete a Service",
           OperationId = "ServicesController.DeleteService",
           Tags = new[] { "ServiceRegister" })
       ]
        [HttpDelete("{ServiceId}")]
        public async Task<ActionResult> Delete(int ServiceId)
        {
            var deleteService = await repository.GetById(ServiceId);
            if (deleteService == null)
            {
                return NotFound();
            }
            await repository.Delete(deleteService);
            return Ok();
        }
    }
}
