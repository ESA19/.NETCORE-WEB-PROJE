using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deneme.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        [Display(Name ="Doktorun Adı")]
        public string DoctorName { get; set; }

        
        
        public int DepartmentId { get; set; }
        
        [Display(Name = "Doktorun Bölümü")]
        public Department Department { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
