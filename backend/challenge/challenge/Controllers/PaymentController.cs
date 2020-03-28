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


        [HttpGet]
        public PagedResult<PaymentDto> GetPayments(int currentPage, int pageSize)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value;
            return _paymentManager.GetPayments(currentPage, pageSize, int.Parse(userId));
        }

    }
}