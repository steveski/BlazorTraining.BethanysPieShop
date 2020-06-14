namespace BethanysPieShop.Server.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using Components;
    using Microsoft.AspNetCore.Components;
    using Services;

    public class EmployeeOverviewBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

        private List<Country> Countries { get; set; }

        private List<JobCategory> JobCategories { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        // This property name HAS to match the @ref name in the template
        protected AddEmployeeDialogBase AddEmployeeDialog { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees().ConfigureAwait(false))?.ToList();

        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

    }

}
