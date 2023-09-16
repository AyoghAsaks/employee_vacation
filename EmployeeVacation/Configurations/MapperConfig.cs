using AutoMapper;
using EmployeeVacation.Models;
using EmployeeVacation.Models.DTO;

namespace EmployeeVacation.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            //CreateMap<Source, Destination>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
        }
    }
}
