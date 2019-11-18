using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalaryCalculator.Entities.ViewModels;

namespace SalaryCalculator.Business
{
    public interface ISalarySupervisor
    {
        Task<ICollection<EmployeeSalaryViewModel>> GetEmployeesSalaries(int[] employeeIds);
    }
}