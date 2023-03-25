using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Contract;
using ServiceLayer.Service.Implementation;
using ServiceLayer.Service.UoW;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            return await Task.Run(() => repository.GetAllBooks());
        }

        [HttpGet]
        [Route("get")]
        public async Task<Book> GetBook(int id)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            return await Task.Run(() => repository.GetBookById(id));
        }

        [HttpPost("add")]
        public async Task AddBook(Book book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;


            await Task.Run(() => repository.AddBook(book));

            _unitOfWork.SaveChanges();
        }

        [HttpPut("edit")]
        public async Task UpdateBook(Book book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;


            await Task.Run(() => repository.UpdateBook(book));

            _unitOfWork.SaveChanges();
        }

        [HttpDelete]
        public async Task DeleteBook(long id)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            await Task.Run(() => repository.RemoveBook(id));
            _unitOfWork.SaveChanges();
        }
    }
}
