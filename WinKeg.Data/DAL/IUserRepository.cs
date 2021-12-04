using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.DAL
{
    public interface IUserRepository : IRepository<User>
    {
        public IEnumerable<User> GetAdministrativeUsers();
        public IEnumerable<User> GetNonRestrictedUsers();
    }
}
