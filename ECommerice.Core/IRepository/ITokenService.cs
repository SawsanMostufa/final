using ECommerice.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerice.Core.IRepository
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
