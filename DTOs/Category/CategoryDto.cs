using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTOs.Category
{
    public class CategoryDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Book Names")]
        public List<string> BookNames { get; set; } = new List<string>();
    }
}