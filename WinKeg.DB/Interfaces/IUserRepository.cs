using System;
using System.Collections.Generic;
using System.Text;
using WinKeg.DB.Models;

namespace WinKeg.DB.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAdministrativeUsers();
        IEnumerable<User> GetNonRestrictedUsers();
    }
}
