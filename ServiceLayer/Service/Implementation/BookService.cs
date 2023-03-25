using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Service.Contract;

namespace ServiceLayer.Service.Implementation
{
    public class BookService : IBook
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Book> GetAllBooks()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBookById(long id)
        {
            return _dbContext.Books.Where(b => b.BookId == id).FirstOrDefault();
        }

        public string AddBook(Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                SaveChanges();

                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateBook(Book book)
        {
            try
            {
                var bookValue = _dbContext.Books.Find(book.BookId);

                if (bookValue != null)
                {
                    bookValue.Title = book.Title;
                    bookValue.Author = book.Author;
                    bookValue.Price = book.Price;
                    _dbContext.Books.Update(bookValue);
                    SaveChanges();
                    return "Successfully Updated";
                }
                else
                {
                    return "No Record(s) Found";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string RemoveBook(long id)
        {
            try
            {
                var book = _dbContext.Books.Where(b => b.BookId == id).FirstOrDefault();
                _dbContext.Books.Remove(book);
                SaveChanges();

                return "Successfully Removed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
