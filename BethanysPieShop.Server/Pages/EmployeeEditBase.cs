namespace BethanysPieShop.Server.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime;
    using System.Threading.Tasks;
    using BethanysPieShopHRM.Shared;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Services;

    public class EmployeeEditBase : ComponentBase
    {
        [Inject] public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject] public ICountryDataService CountryDataService{ get; set; }
        [Inject] public IJobCategoryDataService JobCategoryDataService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }


        [Parameter] public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();


        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        public string Message { get; set; } = string.Empty;
        public string StatusClass { get; set; } = string.Empty;
        public bool Saved;


        protected override async Task OnInitializedAsync()
        {
            Saved = false;

            Countries = (await CountryDataService.GetAllCountries().ConfigureAwait(false))?.ToList();
            JobCategories = (await JobCategoryDataService.GetAllJobCategories().ConfigureAwait(false))?.ToList();

            int.TryParse(EmployeeId, out var employeeId);
            if (employeeId == 0) // new employee is being created
            {
                Employee = new Employee {CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now};
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId)).ConfigureAwait(false);
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();

        }

        protected async Task HandleValidSubmit()
        {
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0) // new
            {
                var addedEmployee = await EmployeeDataService.AddEmployee(Employee).ConfigureAwait(false);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee).ConfigureAwait(false);
                StatusClass = "alert-success";
                Message = "Employee updated successfully";
                Saved = true;
            }
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId).ConfigureAwait(false);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;

        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }

    }
}
