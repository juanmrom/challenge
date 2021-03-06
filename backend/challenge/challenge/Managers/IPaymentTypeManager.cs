﻿using challenge.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public interface IPaymentTypeManager
    {
        IEnumerable<PaymentTypeDto> Get();
    }
}
