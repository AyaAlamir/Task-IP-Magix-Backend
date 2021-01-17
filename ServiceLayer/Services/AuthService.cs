using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityUser> _roleManager;
        private readonly IConfiguration _configration;
        public AuthService(UserManager<IdentityUser> userManager, RoleManager<IdentityUser> roleManager, IConfiguration configration)
        {
            _configration = configration;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> Register(RegisterDto registerDto)
        {
            var userExist = _userManager.FindByNameAsync(registerDto.UserName);
            if (userExist != null)
                return false;
            IdentityUser user = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
                return true;
            return false;

        }
    }
}
