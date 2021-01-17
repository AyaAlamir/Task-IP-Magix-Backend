using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<string> Register(RegisterDto registerDto)
        {
            var userExist = _userManager.FindByNameAsync(registerDto.UserName);
            IdentityUser user = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
                return result.Succeeded.ToString();
            return result.Errors.FirstOrDefault().Description.ToString();

        }
    }
}
