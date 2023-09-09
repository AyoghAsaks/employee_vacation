using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Management.Service.Models
{
    public class ApiResponse<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }

        //New Additions
        public bool IsSuccess { get; set; } //New Addition
        public T? Response { get; set; } //New Addition
    }
}
