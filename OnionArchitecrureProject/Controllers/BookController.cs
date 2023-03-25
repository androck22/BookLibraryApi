using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Service.Contract;

namespace WebAPI_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;

        public BookController(IBook book)
        {
            _book = book;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllBooks()
        {
            var response = _book.GetAllBooks();
            return Ok(response);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult GetBook(int id)
        {
            return Ok(_book.GetBookById(id));
        }

        [HttpPost("add")]
        public IActionResult AddBook(Book book)
        {
            return Ok(_book.AddBook(book));
        }

        [HttpPut("edit")]
        public IActionResult UpdateBook(Book book)
        {
            return Ok(_book.UpdateBook(book));
        }

        [HttpDelete]
        public IActionResult DeleteBook(long id)
        {
            return Ok(_book.RemoveBook(id));
        }
    }
}
