namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using EnsureThat;

    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;

        public CountryDataService(HttpClient httpClient)
        {
            Ensure.Any.IsNotNull(httpClient, nameof(httpClient));

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(
                await _httpClient.GetStreamAsync($"api/country").ConfigureAwait(false),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ).ConfigureAwait(false);
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await JsonSerializer.DeserializeAsync<Country>(
                await _httpClient.GetStreamAsync($"api/country/{countryId}").ConfigureAwait(false),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ).ConfigureAwait(false);
        }
    }
}