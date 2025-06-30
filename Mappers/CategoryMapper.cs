using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DTOs.Category;
using Library.Models;

namespace Library.Mappers
{
    public static class CategoryMapper
    {
        public static Category CreateToCategory(this CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                BookNames = category.Books.Select(b => b.Title).ToList()
            };
        }

        public static CategoryDto UpdateToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                BookNames = category.Books.Select(b => b.Title).ToList()
            };
        }

    }
}