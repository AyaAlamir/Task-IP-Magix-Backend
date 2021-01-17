using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Shared.Enum.CommonEnum;

namespace Infrastructure.Interfaces.Repository.Crud
{
    public interface IReadRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = "");
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null);
        Task<int> GetCountAsync();
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter);
    }
}
