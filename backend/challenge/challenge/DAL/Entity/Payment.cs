using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.DAL.Entity
{
    public class Payment : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public int PaymentTypeId { get; set; }
        public User User { get; set; }
        public PaymentType PaymentType { get; set; }
        public string PlaceName { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
