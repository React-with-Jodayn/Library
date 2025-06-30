using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Category;
using Library.Helpers;
using Library.Models;

namespace Library.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync(CategoryQuery query);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<Category> CreateCategoryAsync(CreateCategoryDto category);

        Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}