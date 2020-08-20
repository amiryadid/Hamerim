using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hamerim.Models;

namespace Hamerim.Services
{
    public interface IPermissionsService
    {
        bool ValidateAdmin(string username, string password);
    }
}