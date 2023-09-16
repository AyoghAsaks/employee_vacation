using EmployeeVacation.Constants;
using EmployeeVacation.Data;
using EmployeeVacation.IRepositories;
using EmployeeVacation.Models;
using EmployeeVacation.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeVacation.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ApplicationDbContext _context;

        //Constructor
        public LeaveAllocationRepository(
            ApplicationDbContext context, 
            UserManager<Employee> userManager,
            ILeaveTypeRepository leaveTypeRepository,
            ApplicationDbContext contxt) : base(context)
        {
            _userManager = userManager;
            _leaveTypeRepository = leaveTypeRepository;
            _context = contxt;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == employeeId
                                                                 && q.LeaveTypeId == leaveTypeId
                                                                 && q.Period == period);
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User); //get all the employees/User
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId); //returns a LeaveType object
            
            //creates a "LeaveAllocation list" in memory called "allocations"
            var allocations = new List<LeaveAllocation>(); //creates a LeaveAllocation list.
            
            /*
            foreach(var employee in employees)
            {
                var allocation = new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    //"leaveType.DefaultDays" gives the LeaveType days
                    NumberOfDays = leaveType.DefaultDays
                };
                await AddAsync(allocation); //adds the new allocation to database.
            }
            */

            //Assign all the employees one by one to the "LeaveAllocation list" in memory
            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period))
                {
                    //if statement is true, do not go to code below but go to next employee in loop.
                    continue; 
                }

                //Assigns new employee info to the LeaveAllocation list in memory one by one.
                //"new LeaveAllocation" is an object that is created and the Added to the list called "allocations"...
                //...This process is done for each employee, i.e., new LeaveAllocation object is created
                //and then added to the list called "allocations" which is in memory.
                //When the foreach loop ends, the list is added to the database
                //using AddRangeAsync method found in the IGenericRepository
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    //"leaveType.DefaultDays" gives the LeaveType days
                    NumberOfDays = leaveType.DefaultDays
                });
            }

            await AddRangeAsync(allocations); //Add allocations list to the database
        }

        ////Vacation Allocation
        /*
        public async Task VacationAllocation(string? leaveTypeName)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User); //get all the employees/User
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetByNameAsync(leaveTypeName); //returns a specific LeaveType object with given name leaveTypeName 

            //creates a "LeaveAllocation list" in memory called "allocations"
            var allocations = new List<LeaveAllocation>(); //creates a LeaveAllocation list.

            //Assign all the employees one by one to the "LeaveAllocation list" in memory
            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveType.Id, period))
                {
                    //if statement is true, do not go to code below but go to next employee in loop.
                    continue;
                }

                //Assigns new employee info to the LeaveAllocation list in memory one by one.
                //"new LeaveAllocation" is an object that is created and the Added to the list called "allocations"...
                //...This process is done for each employee, i.e., new LeaveAllocation object is created
                //and then added to the list called "allocations" which is in memory.
                //When the foreach loop ends, the list is added to the database
                //using AddRangeAsync method found in the IGenericRepository
                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    Period = period,
                    //"leaveType.DefaultDays" gives the LeaveType days
                    NumberOfDays = leaveType.DefaultDays
                });
            }

            await AddRangeAsync(allocations); //Add allocations list to the database
        }
        */
    }   
}
