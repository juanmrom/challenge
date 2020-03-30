using challenge.DAL;
using challenge.DAL.CustomRepository;
using challenge.DAL.Dto;
using challenge.DAL.Entity;
using challenge.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace challenge.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _paymentReposirotry;
        private readonly IRepository<PaymentType> _paymentTypeReposirotry;

        public PaymentManager(IPaymentRepository paymentRepository
            , IRepository<PaymentType> paymentTypeRepository
            )
        {
            _paymentReposirotry = paymentRepository;
            _paymentTypeReposirotry = paymentTypeRepository;
        }

        public IEnumerable<PaymentTypeDto> GetPaymentTypes()
        {            
            var paymentTypes = _paymentTypeReposirotry.GetAll();

            return ConvertPaymentTypeToDto(paymentTypes);
        }

        public PagedResult<PaymentDto> GetPayments(int currentPage, int pageSize, int userId)
        {            
            var payments = _paymentReposirotry.Get(currentPage, pageSize, p => p.UserId == userId);
            
            return ConvertToPaymentDto(payments);
        }

        public void DeletePayments(int[] payments)
        {
            _paymentReposirotry.Delete(payments);
        }

        public void DeletePayment(int id)
        {
            _paymentReposirotry.Delete(id);
        }

        public void UpdatePayment(int id, PaymentDto payment)
        {
            var updatePayment = _paymentReposirotry.Get(id);
            updatePayment.Amount = payment.Amount;
            updatePayment.PaymentDate = payment.PaymentDate;
            updatePayment.PaymentTypeId = payment.PaymentTypeId;
            updatePayment.PlaceName = payment.PlaceName;

            _paymentReposirotry.Update(updatePayment);
        }

        public PaymentDto AddPayment(PaymentDto payment, int userId)
        {
           payment.Id = _paymentReposirotry.Add(new Payment()
            {
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentTypeId = payment.PaymentTypeId,
                PlaceName = payment.PlaceName,
                UserId = userId
            }).Id;
            return payment;
        }

        public IEnumerable<TotalAmount> GetTotalAmount(int userId)
        {
            return _paymentReposirotry.GetQuerable()
                .Where(m => 
                    m.UserId == userId
                )
                .Select(t =>            
                new TotalAmount()
                {
                    PaymentId = t.Id,
                    Amount = t.Amount
                }
            );
        }

        protected PagedResult<PaymentDto> ConvertToPaymentDto(PagedResult<Payment> paymentsResult)
        {
            var paymentResutDto = new PagedResult<PaymentDto>()
            {
                CurrentPage = paymentsResult.CurrentPage,
                PageCount = paymentsResult.PageCount,
                PageSize = paymentsResult.PageSize,
                RowCount = paymentsResult.RowCount
            };
            var paymentDto = new List<PaymentDto>();

            foreach(var payment in paymentsResult.Results)
            {
                paymentDto.Add(
                    new PaymentDto()
                    {
                        Id = payment.Id,
                        Amount = payment.Amount,
                        PaymentTypeId = payment.PaymentTypeId,
                        PlaceName = payment.PlaceName,
                        PaymentTypeName = payment.PaymentType.Name
                    });
            }
            paymentResutDto.Results = paymentDto;

            return paymentResutDto;
        }        

        protected IEnumerable<PaymentTypeDto> ConvertPaymentTypeToDto(IEnumerable<PaymentType> paymentTypes)
        {
            var paymentTypesDto = new List<PaymentTypeDto>();

            foreach(var payment in paymentTypes)
            {
                paymentTypesDto.Add(new PaymentTypeDto()
                {
                    PaymentId = payment.Id,
                    Name = payment.Name
                });
            }

            return paymentTypesDto;
        }
    }
}
