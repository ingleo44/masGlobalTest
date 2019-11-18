using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaryCalculator.DAL.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IQueryable<T>> Query(string path);
    }
}