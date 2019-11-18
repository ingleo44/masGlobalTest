using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalaryCalculator.Entities.Classes;

namespace SalaryCalculator.Entities.Repositories
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> GetEmployeesByIds(int[] ids);
    }
}