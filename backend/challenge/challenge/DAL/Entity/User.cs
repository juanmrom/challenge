using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.DAL.Entity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Names { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Address Direction { get; set; }
    }
}
