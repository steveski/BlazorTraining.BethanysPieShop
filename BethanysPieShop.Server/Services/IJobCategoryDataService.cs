namespace BethanysPieShop.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;

    public interface IJobCategoryDataService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories();
        Task<JobCategory> GetJobCategoryById(int jobCategoryId);

    }
}
