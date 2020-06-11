﻿namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;

    public interface IEmployeeDataService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeDetails(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);

    }
}
