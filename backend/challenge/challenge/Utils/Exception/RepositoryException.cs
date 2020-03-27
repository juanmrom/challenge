using challenge.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Utils.Exception
{
    public class RepositoryException : System.Exception
    {
        public string ErrorNumber { get; set; }
        public RepositoryException()
        {

        }

        public RepositoryException(string message)
            : base(message)
        {

        }

        public RepositoryException(string message, System.Exception e)
            : base(message, e)
        {

        }
    }
}
