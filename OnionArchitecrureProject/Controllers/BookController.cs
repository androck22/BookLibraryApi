using AutoMapper;
using DomainLayer.DTO;
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
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public BookController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<List<AllBooksDto>> GetAllBooks()
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            var books = await Task.Run(() => repository.GetAllBooks());

            var request = _mapper.Map<List<Book>, List<AllBooksDto>>(books.ToList());

            return request;
        }

        [HttpGet]
        [Route("get")]
        public async Task<BookDto> GetBook(int id)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;

            var book = await Task.Run(() => repository.GetBookById(id));

            var request = _mapper.Map<Book, BookDto>(book);

            return request;
        }

        [HttpPost("add")]
        public async Task AddBook(AddBookDto book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;
            var newBook = _mapper.Map<AddBookDto, Book>(book);

            await Task.Run(() => repository.AddBook(newBook));

            _unitOfWork.SaveChanges();
        }

        [HttpPut("edit")]
        public async Task UpdateBook(EditBookDto book)
        {
            var repository = _unitOfWork.GetRepository<Book>() as BookService;
            var editBook = _mapper.Map<EditBookDto, Book>(book);

            await Task.Run(() => repository.UpdateBook(editBook));

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
