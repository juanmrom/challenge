using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.DAL.Dto;
using challenge.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentTypeController : ControllerBase
    {
        readonly IPaymentTypeManager _manager;
        public PaymentTypeController(IPaymentTypeManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IEnumerable<PaymentTypeDto> Get()
        {
            return _manager.Get();
        }
    }
}