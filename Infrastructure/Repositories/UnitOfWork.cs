using Infrastructure.Context;
using Infrastructure.Interfaces.Repository.Common;
using Infrastructure.Interfaces.Repository.Custom;
using Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext AppDbContext { get; }
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configration;
        public UnitOfWork(AppDbContext appContext, UserManager<IdentityUser> userManager, IConfiguration configration)
        {
            AppDbContext = appContext;
            _userManager = userManager;
            _configration = configration;
        }
        public IRepository<Student> Student => new Repository<Student>(AppDbContext);
        public IRepository<ClassRoom> ClassRoom => new Repository<ClassRoom>(AppDbContext);
        public IAuthRepository Auth => new AuthRepository(_userManager , _configration);

        public Task<int> Commit()
        {
            return AppDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
