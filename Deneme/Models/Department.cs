using System.ComponentModel.DataAnnotations;

namespace Deneme.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Display(Name ="Departman Adı")]
        public string DepartmentName { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<Appointments> Appointments { get; set; }= new List<Appointments>();
    }

}
