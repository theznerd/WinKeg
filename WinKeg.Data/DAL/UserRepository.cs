using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly WinKegContext _context;

        public UserRepository(WinKegContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<User> GetAdministrativeUsers()
        {
            return _context.Users.Where(u => u.IsAdministrator).AsEnumerable();
        }

        public IEnumerable<User> GetNonRestrictedUsers()
        {
            return _context.Users.Where(u => !u.IsBeverageRestricted).AsEnumerable();
        }
    }
}
