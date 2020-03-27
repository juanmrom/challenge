using challenge.DAL;
using challenge.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IRepository<User> _userReposirotry;
        private readonly IRepository<Payment> _paymentReposirotry;

        public PaymentManager(IRepository<Payment> repository)
        {
            _paymentReposirotry = repository;
        }
           

    }
}
