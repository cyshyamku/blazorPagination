using AutoMapper;
using Azure;
using BlazorLearning.Data.Data;
using BlazorLearning.Data.Models;
using BlazorLearning.Data.Repository.Interface;
using BlazorLearning.Shared.ViewModel;
using Microsoft.EntityFrameworkCore;
using BlazorPagination;

namespace BlazorLearning.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployeeViewModel> AddEmployee(EmployeeViewModel employee)
        {
            var empDetails = new Employee
            {
                Name = employee.Name,
                Address = employee.Address,
                MobileNumber = employee.MobileNumber,
                EmailId = employee.EmailId
            };
            var empResult = await _dbContext.Employees.AddAsync(empDetails);
            await _dbContext.SaveChangesAsync();
            employee.Id = empResult.Entity.Id;
            return employee;
        }

        public async Task DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<EmployeeViewModel> GetEmployeeData(int id)
        {
            var empResult = await _dbContext.Employees.FindAsync(id);
            if (empResult != null)
            {
                var result = _mapper.Map<EmployeeViewModel>(empResult);
                return result;
            }
            return new EmployeeViewModel();
        }

        public async Task<GridParameterViewModel> GetEmployeeDetails(int page, int pageSize, string sortColumn, string sortDirection)
        {
            try
            {
                var result =  await _dbContext.Employees.AsQueryable().OrderBy(sortColumn, sortDirection)     // custom sort extension to sort on a string representing a column
                .ToPagedResultAsync(page, pageSize);
                var temp = new GridParameterViewModel()
                {
                    CurrentPage = result.CurrentPage,
                    FirstRowOnPage = result.FirstRowOnPage,
                    LastRowOnPage = result.LastRowOnPage,
                    PageCount = result.PageCount,
                    PageSize = result.PageSize,
                    RowCount = result.RowCount,
                    Results = _mapper.Map<List<EmployeeViewModel>>(result.Results),
                };
                return temp;
            }
            catch (Exception ex)
            {
                return new GridParameterViewModel();
            }
           
        }

        public async Task<EmployeeViewModel> UpdateEmployeeDetails(EmployeeViewModel employee)
        {
            var empEntity = _mapper.Map<Employee>(employee);
            _dbContext.Entry(empEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return new EmployeeViewModel();
        }
    }
}
