namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using EnsureThat;

    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;

        public EmployeeDataService(HttpClient httpClient)
        {
            Ensure.Any.IsNotNull(httpClient, nameof(httpClient));

            _httpClient = httpClient;

        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(
                await _httpClient.GetStreamAsync($"api/employee").ConfigureAwait(false),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ).ConfigureAwait(false);
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            return await JsonSerializer.DeserializeAsync<Employee>(
                await _httpClient.GetStreamAsync($"api/employee/{employeeId}").ConfigureAwait(false),
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true}
            ).ConfigureAwait(false);
        }

        public Task<Employee> AddEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateEmployee(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteEmployee(int employeeId)
        {
            throw new System.NotImplementedException();
        }
    }
}
