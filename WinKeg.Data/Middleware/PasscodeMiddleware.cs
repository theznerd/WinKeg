using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HashLibrary;
using System.Threading.Tasks;
using WinKeg.Data.Models;

namespace WinKeg.Data.Middleware
{
    public static class PasscodeMiddleware
    {
        private static bool ValidatePasscode(string passcode, IEnumerable<User> users)
        {
            // Iterate through the users and test for a
            // matching hash.
            foreach (User u in users)
            {
                if (null != u.EncryptedPasscode)
                {
                    HashedPassword userPasscode = new HashedPassword(u.EncryptedPasscode, u.PCSalt);
                    if (userPasscode.Check(passcode))
                    {
                        userPasscode = null;
                        return true; // Passcode matches
                    }
                }
            }
            return false; // No matching passcode
        }

        public static bool ValidateAdmin(string passcode)
        {
            // Gather current list of admin users
            // Since we're not prompting for a username, we
            // have to check the passcode against all users.
            // This is pretty inefficient, but for most
            // personal kegerators, this shouldn't be a big
            // deal
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                return ValidatePasscode(passcode, unitOfWork.Users.GetAdministrativeUsers());
            }
        }

        public static bool ValidateNonRestricted(string passcode)
        {
            // Gather current list of non-restricted users
            // Since we're not prompting for a username, we
            // have to check the passcode against all users.
            // This is pretty inefficient, but for most
            // personal kegerators, this shouldn't be a big
            // deal
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                return ValidatePasscode(passcode, unitOfWork.Users.GetNonRestrictedUsers());
            }
        }

        public static int SetPasscode(string passcode, int id)
        {
            // Check first to make sure we have no other
            // matching passcodes - we can't have duplicates
            // in the system since we're not relying on
            // usernames or IDs
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                bool passcodeAlreadyExists = ValidatePasscode(passcode, unitOfWork.Users.GetAll());
                if (passcodeAlreadyExists)
                {
                    return -1; // choose a different passcode
                }
            }

            HashedPassword hp = HashedPassword.New(passcode);
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                User u = unitOfWork.Users.GetById(id);
                u.EncryptedPasscode = hp.Hash;
                u.PCSalt = hp.Salt;
                u.LastModified = DateTime.Now;
                return unitOfWork.Complete();
            }
        }

        public static string[] SetPasscode(string passcode)
        {
            // Check first to make sure we have no other
            // matching passcodes - we can't have duplicates
            // in the system since we're not relying on
            // usernames or IDs
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                bool passcodeAlreadyExists = ValidatePasscode(passcode, unitOfWork.Users.GetAll());
                if (passcodeAlreadyExists)
                {
                    return null; // choose a different passcode
                }
            }

            HashedPassword hp = HashedPassword.New(passcode);
            return new string[] { hp.Hash, hp.Salt };
        }
    }
}
