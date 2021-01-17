using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configration;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configration)
        {
            _userManager = userManager;
            _configration = configration;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
           
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<Claim> userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email , user.Email)
                };
                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer : _configration["JWT:Issuer"],
                    audience: _configration["JWT:Audeince"],
                    expires: DateTime.Now.AddDays(7),
                    claims: userClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
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
