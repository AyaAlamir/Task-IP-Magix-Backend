using Infrastructure.Context;
using Infrastructure.Interfaces.Repository.Common;
using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext AppDbContext { get; }
        public UnitOfWork(AppDbContext appContext)
        {
            AppDbContext = appContext;
        }
        public IRepository<Student> Student => new Repository<Student>(AppDbContext);
        public IRepository<ClassRoom> ClassRoom => new Repository<ClassRoom>(AppDbContext);

        public Task<int> Commit()
        {
            return AppDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
