using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.DAL.Dto;
using challenge.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateManager _authenticateManager;
        public AuthenticateController(IAuthenticateManager authenticateManager)
        {
            _authenticateManager = authenticateManager;
        }
        [HttpPost]
        public string Authenticate([FromBody] UserDto userCred)
        {
            string token = _authenticateManager.Authenticate(userCred.Username, userCred.Password);
            
            return token;
        }
    }
}