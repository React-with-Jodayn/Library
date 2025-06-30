using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Helpers;
using Library.Interfaces.Repositories;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(BookQuery query)
        {
            {
                var books = _context.Books.Include(b => b.Category).AsQueryable();
                if (!string.IsNullOrWhiteSpace(query.Author))
                    books = books.Where(b => b.Author.Contains(query.Author));
                if (!string.IsNullOrWhiteSpace(query.Title))
                    books = books.Where(b => b.Title.Contains(query.Title));
                if (!string.IsNullOrWhiteSpace(query.CategoryName))
                    books = books.Where(b => b.Category != null && b.Category.Name.Contains(query.CategoryName));

                return await books.ToListAsync();
            }
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
            return _context.Books.Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            //    _context.Books.Update(book);   مش مهم لانه بعمل  track   من لما حكيتله  find  قبل
            await _context.SaveChangesAsync();
            return book;
        }
    }
}