using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}