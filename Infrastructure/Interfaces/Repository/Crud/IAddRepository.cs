using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interfaces.Repository.Crud
{
    public interface IAddRepository<T>
    {
        void CreateAsyn(T entity);
        void CreateListAsyn(List<T> entityList);
    }
}
