using challenge.DAL;
using challenge.DAL.Entity;
using challenge.Utils.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeTest
{
    public class BaseTest 
    {
        protected ChallengeContext _challengeContext;
        protected GenericRepository<User> _userRepository;
        protected GenericRepository<PaymentType> _paymentTypeRepository;
        protected GenericRepository<Payment> _paymentRepository;

        public BaseTest()
        {
            InitializeDatabase();
            InitializeRepository();
        }

        private void InitializeRepository()
        {
            _userRepository = new GenericRepository<User>(_challengeContext);
            _paymentRepository = new GenericRepository<Payment>(_challengeContext);
            _paymentTypeRepository = new GenericRepository<PaymentType>(_challengeContext);
        }

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<ChallengeContext>()
                .UseInMemoryDatabase("PaymentDB")
                .Options;
            
            _challengeContext = new ChallengeContext(options);

            DataHelper.Initialize(_challengeContext);
        }
    }
}
