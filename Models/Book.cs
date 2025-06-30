using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int? CategoryId { get; set; }//FK 
        public Category? Category { get; set; } // 1-M
        public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>(); //M-M


    }
}