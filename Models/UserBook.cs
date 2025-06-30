using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class UserBook
    {


        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int UserId { get; set; }//fk
        public User? User { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }// fk
    }
}