using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Book;
using Library.Models;

namespace Library.Mappers
{
    public static class BookMapper
    {
        public static Book CreateToBook(this CreateBookDto bookDto, int categoryId)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                CategoryId = categoryId,
            };
        }
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
                CategoryName = book.Category?.Name ?? string.Empty,
            };
        }

    }
}