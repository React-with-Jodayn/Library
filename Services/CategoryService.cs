using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Category;
using Library.Helpers;
using Library.Interfaces.Repositories;
using Library.Interfaces.Services;
using Library.Mappers;
using Library.Models;

namespace Library.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepo.CategoryExistsAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var newCategory = categoryDto.CreateToCategory();
            return await _categoryRepo.CreateCategoryAsync(newCategory);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepo.DeleteCategoryAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync(CategoryQuery query)
        {
            return await _categoryRepo.GetAllCategoriesAsync(query);
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepo.GetCategoryByIdAsync(id);
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            return await _categoryRepo.UpdateCategoryAsync(id, categoryDto);
        }
    }
}