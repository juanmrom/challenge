﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.DAL.Dto
{
    public class PaymentDto
    {
        public int UserId { get; set; }        
        public int PaymentTypeId { get; set; }
        public string PlaceName { get; set; }
        public double Amount { get; set; }
    }
}
