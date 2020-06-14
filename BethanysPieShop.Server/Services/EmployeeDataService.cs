namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using EnsureThat;
    using Microsoft.AspNetCore.Mvc.Formatters;

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

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, MediaType.Json);
            var response = await _httpClient.PostAsync("api/employee", employeeJson).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Employee>(
                    await response.Content.ReadAsStreamAsync().ConfigureAwait(false)
                    ).ConfigureAwait(false);
            }

            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var employeeJson = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, MediaType.Json);
            await _httpClient.PutAsync("api/employee", employeeJson).ConfigureAwait(false);
        }

        public Task DeleteEmployee(int employeeId)
        {
            return _httpClient.DeleteAsync($"api/employee/{employeeId}");
        }
    }
}
