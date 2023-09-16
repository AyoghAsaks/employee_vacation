using EmployeeVacation.Data;
using EmployeeVacation.IRepositories;
using EmployeeVacation.Models;

namespace EmployeeVacation.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(ApplicationDbContext context) : base(context) 
        {
        }
    }
}
