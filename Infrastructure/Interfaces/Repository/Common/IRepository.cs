using Infrastructure.Interfaces.Repository.Crud;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repository.Common
{
    public interface IRepository<T> : IReadRepository<T>, IAddRepository<T>, IEditRepository<T>, IDeleteRepository<T>
    {
    }
}
