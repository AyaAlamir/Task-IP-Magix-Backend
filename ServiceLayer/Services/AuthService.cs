using Infrastructure.Interfaces.Repository.Common;
using Infrastructure.Model.Auth;
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
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<string> Login(LoginDto loginDto)
        {
            LoginModel loginModel = new LoginModel
            {
                UserName = loginDto.UserName,
                Password = loginDto.Password
            };
            string result = await _unitOfWork.Auth.Login(loginModel);         
            return result;
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            RegisterModel registerModel = new RegisterModel
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Email = registerDto.Email
            };
            string result = await _unitOfWork.Auth.Register(registerModel);
            return result;

        }
    }
}
