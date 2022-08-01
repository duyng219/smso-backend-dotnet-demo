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
    public class ServicesCategoryController : ControllerBase
    {
        private readonly IRepository<ServiceCategory> repository;
        public ServicesCategoryController(IRepository<ServiceCategory> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<ServiceCategory>>> GetAll()
        {
            var user = await repository.ListAll();
            return Ok(user);
        }
        //Lấy dữ liệu
        [HttpGet("{CategoryId}")]
        [SwaggerOperation(
           Summary = "Get all Category",
           Description = "Get Category",
           OperationId = "ServicesCategory.GetAllCategory",
           Tags = new[] { "ServicesCategory" })
       ]
        public async Task<ActionResult<ServiceCategory>> GetCategory(int CategoryId)
        {
            var categories = await repository.Get(b => b.CategoryId == CategoryId);

            if (categories.Count > 0)
            {
                return Ok(categories[0]);
            }
            else
            {
                return NotFound();
            }
        }
        //Thêm 
        [HttpPost]
        [SwaggerOperation(
           Summary = "Insert a Category",
           Description = "Insert a Category",
           OperationId = "ServicesCategory.CreateCategory",
           Tags = new[] { "ServicesCategory" })
       ]
        public async Task<ActionResult<ServiceCategory>> InsertItem(ServiceCategory item)
        {
            var categories = await repository.Insert(item);
            return categories;
        }
        //Sửa
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update a Category",
            Description = "Update a Category",
            OperationId = "ServicesCategory.UpdateCategory",
            Tags = new[] { "ServicesCategory" })
        ]
        public async Task<ActionResult<ServiceCategory>> UpdateItem(ServiceCategory item)
        {

            var categories = await repository.Update(item);
            return categories;
        }
        //xóa
        [SwaggerOperation(
           Summary = "Delete a Category",
           Description = "Delete a Category",
           OperationId = "ServicesCategory.DeleteCategory",
           Tags = new[] { "ServicesCategory" })
       ]
        [HttpDelete("{CategoryId}")]
        public async Task<ActionResult> Delete(int CategoryId)
        {
            var deleteCategory = await repository.GetById(CategoryId);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            await repository.Delete(deleteCategory);
            return Ok();
        }
    }
}
