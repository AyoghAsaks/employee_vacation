using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeVacation.Models
{
    public class LeaveAllocation
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; } //one leaveType can assigned to many LeaveAllocation(Employees)

        public int LeaveTypeId { get; set; }

        public string EmployeeId { get; set; }

        //public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Period { get; set; }
    }
}
