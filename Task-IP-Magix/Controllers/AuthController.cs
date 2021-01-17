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
        /// <returns>true if added successfully , false otherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            string result = await _authService.Register(registerDto);
            return Ok(result);
        }

    }
}