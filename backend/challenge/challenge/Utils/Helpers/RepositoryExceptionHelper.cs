using challenge.Utils.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Utils.Helpers
{
    /// <summary>
    /// Use after return correct language.
    /// </summary>
    public class RepositoryExceptionHelper
    {
        public static void ThrowRepositoryException(string message, string errorNumber)
        {
            throw new RepositoryException(message) { ErrorNumber = errorNumber };
        }
    }
}
