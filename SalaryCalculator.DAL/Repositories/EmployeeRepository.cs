using SalaryCalculator.Entities.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalaryCalculator.Entities.Repositories;

namespace SalaryCalculator.DAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        public async Task<ICollection<Employee>> GetEmployeesByIds(int[] ids)
        {
            var employeesList = await Query("http://masglobaltestapi.azurewebsites.net/api/Employees");
            return ids.Length > 0
                ? employeesList.Where(q => ids.Contains(q.Id))
                    .ToList()
                : employeesList.ToList();
        }
    }
}
