using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        IRepository<Student> Student { get; }
        IRepository<ClassRoom> ClassRoom { get; }
    }
}
