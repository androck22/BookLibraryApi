using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.Contract
{
    public interface IBook
    {
        List<Book> GetAllBooks();
        Book GetBookById(long id);
        string AddBook(Book book);
        string UpdateBook(Book book);
        string RemoveBook(long id);
        void SaveChanges();
    }
}
