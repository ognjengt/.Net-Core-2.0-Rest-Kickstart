using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<Response<string>> SignIn(User user)
        {
            Response<string> response = new Response<string>();

            return response.Success("test", "testtoken");
        }
    }
}