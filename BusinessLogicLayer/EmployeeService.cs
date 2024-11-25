using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private const double totalSalaryBudget = 1000000;
        private const double maxSalaryLimit = 100000;
        private const int maxEngineers = 5;
        private const int maxManagers = 5;
        private const int maxHR = 1;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //Implement the method from the interface, and here I can add logic to whichever methods I want 
        //to add logic to
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
           
            return await _employeeRepository.GetEmployeesAsync();
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task AddEmployeeAsync(Employee employee)
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            double currentTotalSalaries = employees.Sum(e => e.Salary);
            int currentEngineers = employees.Count(e => e.Position == "Engineer");
            int currentManagers = employees.Count(e => e.Position == "Manager");
            int currentHR = employees.Count(e => e.Position == "HR");

            if (currentTotalSalaries + employee.Salary > totalSalaryBudget)
            {
                return;
            }

            if (employee.Salary > maxSalaryLimit)
            {
                return;
            }

            if (employee.Position == "Engineer" && currentEngineers >= maxEngineers)
            {
                return;
            }

            if (employee.Position == "Manager" && currentManagers >= maxManagers)
            {
                return;
            }

            if (employee.Position == "HR" && currentHR >= maxHR)
            {
                return;
            }

            await _employeeRepository.AddEmployeeAsync(employee);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        //going to the DAL, and getting the employee from the employees DbSet
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task GetDetailsAsync(Employee employee)
        {
            await _employeeRepository.GetDetailsAsync(employee);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (employee.Salary <= 100)
            {
                await _employeeRepository.UpdateEmployeeAsync(employee);
            }
        }

    }
}
