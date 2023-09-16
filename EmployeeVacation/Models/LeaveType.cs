using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeVacation.Models
{
    public class LeaveType
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int DefaultDays { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
