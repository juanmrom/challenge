using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using challenge.DAL.Dto;
using challenge.Managers;
using challenge.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentManager _paymentManager;

        public PaymentController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }


        [HttpPost("GetPayments")]
        public PagedResult<PaymentDto> GetPayments(PagedRequest pagedRequest)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value;
            return _paymentManager.GetPayments(pagedRequest.CurrentPage, pagedRequest.PageSize, int.Parse(userId));
        }

        [HttpPost("AddPayment")]
        public PaymentDto AddPayment(PaymentDto payment)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value;            
            return _paymentManager.AddPayment(payment, int.Parse(userId));
        }

        [HttpPost("DeletePayments")]
        public void DeletePayments(int[] id)
        {
            _paymentManager.DeletePayments(id);
        }

        [HttpPut("{id}")]
        public void UpdatePayment(int id, PaymentDto payment)
        {
            _paymentManager.UpdatePayment(id, payment);
        }

        [HttpDelete("{id}")]
        public void DeletePayment(int id)
        {
            _paymentManager.DeletePayment(id);
        }

        [HttpGet("GetTotalAmount")]
        public IEnumerable<TotalAmount> GetTotalAmount()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value;
            return _paymentManager.GetTotalAmount(int.Parse(userId));
        }
    }
}