using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Book;
using Library.Helpers;
using Library.Interfaces.Services;
using Library.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly ICategoryService _categoryService;
        public BookController(IBookService bookService, ILogger<BookController> logger, ICategoryService categoryService)
        {
            _bookService = bookService;
            _logger = logger;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookQuery query)
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync(query);
                var booksDto = books.Select(b => b.ToBookDto()).ToList();
                return Ok(booksDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound();

                return Ok(book.ToBookDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the book.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("{categoryId:int}")]
        public async Task<IActionResult> Create([FromRoute] int categoryId, [FromBody] CreateBookDto bookDto)
        {
            try
            {
                if (!await _categoryService.CategoryExistsAsync(categoryId))
                {
                    _logger.LogWarning("Category with ID {CategoryId} does not exist.", categoryId);
                    return BadRequest("Category does not exist.");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for CreateBookDto.");
                    return BadRequest(ModelState);
                }
                var createdBook = bookDto.CreateToBook(categoryId);
                await _bookService.CreateBookAsync(createdBook);
                return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook.ToBookDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the book.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookDto bookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for CreateBookDto.");
                    return BadRequest(ModelState);
                }
                var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);
                if (updatedBook == null)
                {
                    _logger.LogWarning("Book with ID {Id} does not exist.", id);
                    return NotFound();
                }

                return Ok(updatedBook.ToBookDto());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book.");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var deleted = await _bookService.DeleteBookAsync(id);
                if (!deleted)
                    return NotFound();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}