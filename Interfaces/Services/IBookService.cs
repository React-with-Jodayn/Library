using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Book;
using Library.Helpers;
using Library.Models;

namespace Library.Interfaces.Services
{
    public interface IBookService
    {
        // the diffrence between <IEnumerable<Book>> & <List<Book>> is that IEnumerable is more general and can be used with any collection type, while List is a specific type of collection.
        Task<IEnumerable<Book>> GetAllBooksAsync(BookQuery query);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task<Book?> UpdateBookAsync(int id, UpdateBookDto bookDto);

        Task<bool> DeleteBookAsync(int id);
    }
}