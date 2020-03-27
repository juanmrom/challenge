using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.DAL.Entity
{
    public class Payment : BaseEntity
    {
        public User User { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PlaceName { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
