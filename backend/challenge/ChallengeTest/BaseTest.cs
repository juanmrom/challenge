using challenge.DAL;
using challenge.DAL.CustomRepository;
using challenge.DAL.Entity;
using challenge.Utils.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChallengeTest
{
    public class BaseTest 
    {
        protected ChallengeContext _challengeContext;
        protected GenericRepository<User> _userRepository;
        protected GenericRepository<PaymentType> _paymentTypeRepository;
        protected IPaymentRepository _paymentRepository;

        public BaseTest()
        {
            InitializeDatabase();
            InitializeRepository();
        }

        private void InitializeRepository()
        {
            _userRepository = new GenericRepository<User>(_challengeContext);
            _paymentTypeRepository = new GenericRepository<PaymentType>(_challengeContext);
            //_paymentRepository = new PaymentRepository(_challengeContext);
        }

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<ChallengeContext>()
                .UseInMemoryDatabase("PaymentDB")
                .Options;
            
            _challengeContext = new ChallengeContext(options);

            DataHelper.Initialize(_challengeContext);
            
            var temp = _challengeContext.Payments.Include(u => u.User).ToList();
        }
    }
}
