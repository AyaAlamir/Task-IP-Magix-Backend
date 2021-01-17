using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using Shared.Helper;

namespace Task_IP_Magix.Controllers
{
    [Route(APIRoute.Template)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>true if added successfully , errors otherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            string result = await _authService.Register(registerDto);
            return Ok(result);
        }


        /// <summary>
        /// login user 
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>token if logged successfully , errors otherwize</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            string result = await _authService.Login(loginDto);
            if (result != null)
                return Ok(result);
            return Unauthorized();
        }

    }
}