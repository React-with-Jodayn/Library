using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Category;
using Library.Helpers;
using Library.Models;

namespace Library.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(CategoryQuery query);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<Category> CreateCategoryAsync(Category category);

        Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}