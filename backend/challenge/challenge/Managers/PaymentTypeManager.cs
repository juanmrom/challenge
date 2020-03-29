using challenge.DAL;
using challenge.DAL.Dto;
using challenge.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public class PaymentTypeManager : IPaymentTypeManager
    {
        readonly IRepository<PaymentType> _repository;
        public PaymentTypeManager(IRepository<PaymentType> repository)
        {
            _repository = repository;
        }

        public IEnumerable<PaymentTypeDto> Get()
        {
            var payment = _repository.GetAll().ToArray();

            return payment.Select(m => new PaymentTypeDto() { Name = m.Name, PaymentId = m.Id }).ToArray();
        }
    }
}
