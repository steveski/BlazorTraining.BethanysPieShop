namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;

    public interface ICountryDataService
    {
        Task<IEnumerable<Country>> GetAllCountries();
        Task<Country> GetCountryById(int countryId);

    }
}