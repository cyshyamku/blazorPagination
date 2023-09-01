using AutoMapper;
using BlazorLearning.Data.Models;
using BlazorLearning.Shared.ViewModel;

namespace BlazorLearning.Server
{
    public class MapperClass: Profile
    {
        public MapperClass() {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
        }
       
    }
}
