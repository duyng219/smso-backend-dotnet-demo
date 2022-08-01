using BackEnd.DTO;
using BackEnd.Models;
using BackEnd.Repository;
using BackEnd.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IRepository<tbUser> repository;
        public LoginsController(IRepository<tbUser> repository)
        {
            this.repository = repository;

        }

        [HttpPost]
        [Route("Login")]
        [SwaggerOperation(
                   Summary = "Login to System",
                   Description = "Login to System",
                   OperationId = "UserController.Login",
                   Tags = new[] { "UserController" })]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var userSpec = new UserSpec(request.Email, request.Password);
            var user = await repository.GetAsyncSpec(userSpec);
            if (user != null)
            {
                var res = new LoginResponse();
                res.UserId = user.UserId;
                res.UserName = user.Username;
                res.Phone = user.Phone;
                res.Age = user.Birthday;
                res.Role = user.IsAdmin;// load tu db
                res.Token = "Token";
                res.RefreshToken = "refreshtoken";
                return (res);
            }
            return BadRequest(new LoginResponse());

        }



    }
}
