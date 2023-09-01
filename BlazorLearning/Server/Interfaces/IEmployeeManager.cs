﻿
using BlazorLearning.Shared.ViewModel;

namespace BlazorLearning.Server.Interfaces
{
    public interface IEmployeeManager
    {
        Task<GridParameterViewModel> GetEmployeeDetails(int page, int pageSize, string sortColumn, string sortDirection);
        Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employee);
        Task<EmployeeViewModel> UpdateEmployeeDetails(EmployeeViewModel employee);
        Task<EmployeeViewModel> GetEmployeeData(int id);
        Task DeleteEmployee(int id);
    }
}
