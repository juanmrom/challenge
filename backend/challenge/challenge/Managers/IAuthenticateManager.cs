using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public interface IAuthenticateManager
    {
        string Authenticate(string username, string password);
    }
}
