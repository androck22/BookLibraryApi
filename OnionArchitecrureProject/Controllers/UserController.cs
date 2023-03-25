using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Contract;
using ServiceLayer.Service.Implementation;
using ServiceLayer.Service.UoW;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var repository = _unitOfWork.GetRepository<User>() as UserService;

            return await Task.Run(() => repository.GetAllUsers());
        }

        [HttpGet]
        [Route("get")]
        public async Task<User> GetUser(int id)
        {
            var repository = _unitOfWork.GetRepository<User>() as UserService;

            return await Task.Run(() => repository.GetUserById(id));
        }

        [HttpPost("add")]
        public async Task AddUser(User user)
        {
            var repository = _unitOfWork.GetRepository<User>() as UserService;


            await Task.Run(() => repository.AddUser(user));

            _unitOfWork.SaveChanges();
        }

        [HttpPut("edit")]
        public async Task UpdateUser(User user)
        {
            var repository = _unitOfWork.GetRepository<User>() as UserService;


            await Task.Run(() => repository.UpdateUser(user));

            _unitOfWork.SaveChanges();
        }

        [HttpDelete]
        public async Task DeleteUser(long id)
        {
            var repository = _unitOfWork.GetRepository<User>() as UserService;

            await Task.Run(() => repository.RemoveUser(id));
            _unitOfWork.SaveChanges();
        }
    }
}
