using System.ComponentModel;

namespace PES_EdTech_APP.Models
{
    public class EmployeeViewModel
    {
        public int emp_id { get; set; }
        [DisplayName("Employee Name")]
        public string? ename { get; set; }
        public string? email { get; set; }
    }
}
