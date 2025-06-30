using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.DTOs.Category;
using Library.Helpers;
using Library.Interfaces.Repositories;
using Library.Mappers;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false; // Category not found

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true; // Category deleted successfully
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CategoryQuery query)
        {
            var categories = _context.Categories.Include(c => c.Books).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SortBy))
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    categories = query.IsDescending ? categories.OrderByDescending(c => c.Name) : categories.OrderBy(c => c.Name);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return null;
            category.Name = categoryDto.Name;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}