using Deneme.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deneme.Models
{
    public class Appointment
    {
        
        public int AppointmentId { get; set; }
        [Display(Name ="Hasta Adı")]
        public string PatientName { get; set;}
       

        public int DoctorId { get; set; }
        [Display(Name ="Doktorun Adı")]
        public Doctor Doctor { get; set; }

        public int DepartmentId { get; set; }
        [Display(Name ="Departman Adı")]
        public Department Department { get; set; }

        [Display(Name = "Randevu Tarihi")]
        public DateTime AppointmentDate { get; set; }




    }
}
