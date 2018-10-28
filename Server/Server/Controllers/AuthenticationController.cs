using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Responses;

namespace Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<AuthentificationResult> SignIn(User user)
        {
            AuthentificationResult response = new AuthentificationResult();

            return response.Success("test", null);
        }
    }
}