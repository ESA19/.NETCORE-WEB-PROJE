using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Deneme.Models
{
    public class Doctor
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Display(Name ="Doktorun Adı")]
        public string DoctorName { get; set; }

        [Required]
        [Display(Name = "Doktorun Bölümü")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Appointments> Appointments { get; set; } = new List<Appointments>();
    }
}
