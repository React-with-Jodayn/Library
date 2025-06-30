using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Book;
using Library.Helpers;
using Library.Interfaces.Repositories;
using Library.Interfaces.Services;
using Library.Models;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        public BookService(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            book.CreatedAt = DateTime.UtcNow;
            return await _bookRepo.CreateBookAsync(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null)
                return false;

            return await _bookRepo.DeleteBookAsync(book);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(BookQuery query)
        {
            return await _bookRepo.GetAllBooksAsync(query);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepo.GetBookByIdAsync(id);
        }

        public async Task<Book?> UpdateBookAsync(int id, UpdateBookDto bookDto)
        {
            Book? book = await _bookRepo.GetBookByIdAsync(id);
            if (book == null)
            {
                return null;
            }
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.UpdatedAt = DateTime.UtcNow;
            return await _bookRepo.UpdateBookAsync(book);


        }
    }
}