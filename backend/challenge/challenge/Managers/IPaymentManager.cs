using challenge.DAL.Dto;
using challenge.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Managers
{
    public interface IPaymentManager
    {
        IEnumerable<PaymentTypeDto> GetPaymentTypes();
        PagedResult<PaymentDto> GetPayments(int currentPage, int pageSize, int userId);
        void DeletePayments(int[] payments);
        void DeletePayment(int id);
        void UpdatePayment(int id, PaymentDto payment);
        IEnumerable<TotalAmount> GetTotalAmount(int userId);
        PaymentDto AddPayment(PaymentDto payment, int userId);
    }
}
