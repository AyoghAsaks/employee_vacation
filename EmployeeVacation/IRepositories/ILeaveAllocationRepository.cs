using EmployeeVacation.Models;

namespace EmployeeVacation.IRepositories
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task LeaveAllocation(int leaveTypeId);
        Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period);

        //Task VacationAllocation(string? leaveTypeName);
    }
}
