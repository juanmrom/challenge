using challenge.DAL;
using challenge.DAL.Entity;
using challenge.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ChallengeTest.Managers
{
    public class PaymentManagerTest : BaseTest
    {
        protected PaymentManager PaymentManager
        {
            get;
            private set;
        }

        public PaymentManagerTest() 
            : base()
        {
            PaymentManager = new PaymentManager(_paymentRepository, _paymentTypeRepository);
        }

        [Fact]
        public void GetAll_PaymentTypes_Database()
        {
            var paymentTypes = PaymentManager.GetPaymentTypes();            

            Assert.Equal<int>(9, paymentTypes.ToArray().Length);
        }

        [Fact]
        public void GetPayments()
        {
            var payments = PaymentManager.GetPayments(1, 10, 1);

            Assert.Equal<int>(10, payments.RowCount);
        }
    }
}
