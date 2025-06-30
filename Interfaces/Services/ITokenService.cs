using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}