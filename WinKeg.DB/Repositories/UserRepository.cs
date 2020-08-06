using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;
using WinKeg.DB.Interfaces;
using System.Linq;

namespace WinKeg.DB.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WinKegContext context) : base(context) { }

        public IEnumerable<User> GetAdministrativeUsers()
        {
            return WinKegContext.Users.Where(u => u.IsAdministrator).ToList();
        }

        public IEnumerable<User> GetNonRestrictedUsers()
        {
            return WinKegContext.Users.Where(u => !u.IsBeverageRestricted).ToList();
        }

        public WinKegContext WinKegContext
        {
            get { return Context as WinKegContext; }
        }
    }
}
