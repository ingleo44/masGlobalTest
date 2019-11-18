using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalaryCalculator.Entities.Repositories;
using SalaryCalculator.Entities.ViewModels;

namespace SalaryCalculator.Business
{
    public class SalarySupervisor : ISalarySupervisor
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SalarySupervisor(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ICollection<EmployeeSalaryViewModel>> GetEmployeesSalaries(int[] employeeIds)
        {
            var employeeList = await _employeeRepository.GetEmployeesByIds(employeeIds);

            var result = employeeList.Select(employee => new EmployeeSalaryViewModel
            {
                Id = employee.Id, Name = employee.Name,
                Salary = (employee.ContractTypeName == "HourlySalaryEmployee"
                    ? 120 * employee.HourlySalary * 12
                    : employee.MonthlySalary * 12)
            }).ToList();
            return result;
        }
    }
}