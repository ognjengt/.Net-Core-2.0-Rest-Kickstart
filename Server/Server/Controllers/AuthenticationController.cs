using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Helpers;
using Server.Models;
using Server.Repositories.Implementations;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IAuthentificationRepository _authRepository = new AuthentificationRepository();

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<Response<bool>> Register(User user)
        {
            return await _authRepository.Register(user);
        }


        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<Response<string>> SignIn(User user)
        {
            return await _authRepository.SignIn(user);
        }
    }
}