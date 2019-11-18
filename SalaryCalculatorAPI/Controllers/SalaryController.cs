using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalaryCalculator.Business;

namespace SalaryCalculatorAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ISalarySupervisor _salarySupervisor;

        /// <inheritdoc />
        public SalaryController(ISalarySupervisor salarySupervisor)
        {
            _salarySupervisor = salarySupervisor;
        }

        // POST: api/Salary
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int[] employeesIds)
        {
            var salaries = await _salarySupervisor.GetEmployeesSalaries(employeesIds);
            return new ObjectResult(salaries);
        }
    }
}
