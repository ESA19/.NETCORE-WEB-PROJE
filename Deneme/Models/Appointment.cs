using Deneme.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme.Models
{
    public class Appointment
    {
        
        public int AppointmentId { get; set; }

        [Display(Name = "Hasta Adı")]
        public string UserId { get; set;}

        [ForeignKey("UserId")]

        public ApplicationUser User { get; set; }

        [Display(Name = "Doktorun Adı")]
        public int DoctorId { get; set; }
        
        public Doctor Doctor { get; set; }
        [Display(Name = "Departman Adı")]
        public int DepartmentId { get; set; }
        
        public Department Department { get; set; }

        [Display(Name = "Randevu Tarihi")]
        public DateTime AppointmentDate { get; set; }




    }
}
