using Infrastructure.Model.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repository.Custom
{
    public interface IAuthRepository
    {
        Task<string> Register(RegisterModel registerModel);
        Task<string> Login(LoginModel LoginModel);
    }
}
