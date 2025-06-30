using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    public class User : IdentityUser<int>
    {
        public UserProfile? UserProfile { get; set; }  // One-to-One
        public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>(); //M-M

    }
}