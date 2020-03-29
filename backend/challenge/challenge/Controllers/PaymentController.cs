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

        [HttpPost("DeletePayments")]
        public void DeletePayments(int[] id)
        {
            _paymentManager.DeletePayments(id);
        }

        [HttpPut]
        public void UpdatePayment(PaymentDto payment)
        {
            _paymentManager.UpdatePayment(payment);
        }

        [HttpDelete]
        public void DeletePayment(int id)
        {
            _paymentManager.DeletePayment(id);
        }
    }
}