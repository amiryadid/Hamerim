using System;
using System.Linq;
using Hamerim.Data;

namespace Hamerim.Services
{
    class PermissionsService : IPermissionsService
    {
        public bool ValidateUser(string username, string password)
        {
            using (var ctx = new HamerimDbContext())
            {
                return ctx.Users.Any(user => user.Username == username &&
                                             user.Password == password);
            }
        }

        public bool ValidateAdmin(string username, string password)
        {
            using (var ctx = new HamerimDbContext())
            {
                return ctx.Users.Any(user => user.Username == username &&
                                             user.Password == password &&
                                             user.IsAdmin);
            }
        }

    }
}