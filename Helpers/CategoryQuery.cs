using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Helpers
{
    public class CategoryQuery
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
        [Range(1, 100, ErrorMessage = "Page number must be between 1 and 100.")]
        public int PageNumber { get; set; } = 1;
        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
        public int PageSize { get; set; } = 10;

    }
}