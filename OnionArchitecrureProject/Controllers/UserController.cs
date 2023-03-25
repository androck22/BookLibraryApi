using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Contract;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllUsers()
        {
            var response = _user.GetAllUsers();
            return Ok(response);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetUser(int id)
        {
            return Ok(_user.GetUserById(id));
        }

        [HttpPost("add")]
        public IActionResult AddUser(User user)
        {
            return Ok(_user.AddUser(user));
        }

        [HttpPut("edit")]
        public IActionResult UpdateUser(User user)
        {
            return Ok(_user.UpdateUser(user));
        }

        [HttpDelete]
        public IActionResult DeleteUser(long id)
        {
            return Ok(_user.RemoveUser(id));
        }
    }
}
