using Infrastructure.Context;
using Infrastructure.Interfaces.Repository.Custom;
using Infrastructure.Model.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configration;
        public AuthRepository(UserManager<IdentityUser> userManager, IConfiguration configration)
        {
            _userManager = userManager;
            _configration = configration;
        }
        public async Task<string> Login(LoginModel loginModel)
        {


            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                List<Claim> userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email , user.Email)
                };
                var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configration["JWT:Issuer"],
                    audience: _configration["JWT:Audeince"],
                    expires: DateTime.Now.AddDays(7),
                    claims: userClaims,
                    signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }

        public async Task<string> Register(RegisterModel registerModel)
        {
            var userExist =await _userManager.FindByNameAsync(registerModel.UserName);
            IdentityUser user = new IdentityUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email
            };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
                return result.Succeeded.ToString();
            return result.Errors.FirstOrDefault().Description.ToString();

        }
    }
}
