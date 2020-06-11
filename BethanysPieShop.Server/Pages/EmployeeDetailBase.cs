namespace BethanysPieShop.Server.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using Microsoft.AspNetCore.Components;
    using Services;

    public class EmployeeDetailBase : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId)).ConfigureAwait(false);

        }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public IEnumerable<Employee> Employees { get; set; }

        private List<Country> Countries { get; set; }

        private List<JobCategory> JobCategories { get; set; }



    }
}
