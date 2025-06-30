using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Category
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}