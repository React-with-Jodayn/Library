using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Helpers;
using Library.Models;

namespace Library.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateBookAsync(Book book);
        Task<IEnumerable<Book>> GetAllBooksAsync(BookQuery query);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(Book book);
    }
}