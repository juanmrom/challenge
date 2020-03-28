using challenge.DAL;
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
        private readonly IRepository<User> _userReposirotry;
        private readonly IRepository<Payment> _paymentReposirotry;
        private readonly IRepository<PaymentType> _paymentTypeReposirotry;

        public PaymentManager(IRepository<Payment> paymentRepository
            , IRepository<PaymentType> paymentTypeRepository
            , IRepository<User> userRepository)
        {
            _paymentReposirotry = paymentRepository;
            _paymentTypeReposirotry = paymentTypeRepository;
            _userReposirotry = userRepository;
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
                        Amount = payment.Amount,
                        PaymentTypeId = payment.PaymentTypeId,
                        PlaceName = payment.PlaceName,
                        UserId = payment.UserId
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
