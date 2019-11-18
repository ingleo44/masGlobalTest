using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SalaryCalculator.Business;
using SalaryCalculator.Entities.Classes;
using SalaryCalculator.Entities.Repositories;
using Xunit;

namespace XUnitTestProject
{
    public class SalarySupervisorUnitTest
    {

        private readonly Mock<IEmployeeRepository> _employeeRepository;

        public SalarySupervisorUnitTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            var responseObject = new List<Employee>
            {
                new Employee
                {
                    ContractTypeName = "HourlySalaryEmployee",
                    HourlySalary = 60000,
                    Id = 1,
                    MonthlySalary = 80000,
                    Name = "Juan",
                    RoleDescription = null,
                    RoleId = 1,
                    RoleName = "Administrator"
                },
                new Employee
                {
                    ContractTypeName = "MonthlySalaryEmployee",
                    HourlySalary = 60000,
                    Id = 2,
                    MonthlySalary = 80000,
                    Name = "Sebastian",
                    RoleDescription = null,
                    RoleId = 2,
                    RoleName = "Contractor"
                }
            };
            ICollection<Employee> employees = responseObject;
            _employeeRepository.Setup(q => q.GetEmployeesByIds(It.IsAny<int[]>())).Returns((int[] arr) => QueryList(employees, arr));
        }


        [Fact]
        public async Task sendEmptyArray_ShouldReturnTrue()
        {
            // Arrange
            var emptyArray = new int[0];
            var supervisor = new SalarySupervisor(_employeeRepository.Object);
            var result = await supervisor.GetEmployeesSalaries(emptyArray);
            Assert.True(result.Count == 2);

            // Act 
        }

        [Fact]
        public async Task sendArrayWithKnownValues_ShouldReturnTrue()
        {
            // Arrange
            var array = new[]{1};
            var supervisor = new SalarySupervisor(_employeeRepository.Object);
            var result = await supervisor.GetEmployeesSalaries(array);
            Assert.True(result.Count == 1);

        }

        [Fact]
        public async Task sendArrayWithUnknownValues_ShouldReturnTrue()
        {
            // Arrange
            var array = new[] { 6,8,4 };
            var supervisor = new SalarySupervisor(_employeeRepository.Object);
            var result = await supervisor.GetEmployeesSalaries(array);
            Assert.True(result.Count == 0);

        }


        [Fact]
        public async Task sendArrayWithMixedValues_ShouldReturnTrue()
        {
            // Arrange
            //  The number 1 and 2 are existing records the another ids are not known so it wont return any value
            var array = new[] {1, 6, 8, 4 ,2};
            var supervisor = new SalarySupervisor(_employeeRepository.Object);
            var result = await supervisor.GetEmployeesSalaries(array);
            Assert.True(result.Count == 2);

        }
        private static Task<ICollection<Employee>> QueryList(ICollection<Employee> initialList, int[] filterArray)
        {
            if (filterArray.Length == 0)
            {
                return Task.FromResult(initialList);
            }
            var r = initialList.Where(q => filterArray.Contains(q.Id)).ToList();
            ICollection<Employee> filteredList  = r.ToList();
            return Task.FromResult(filteredList);

        }

    }
}
