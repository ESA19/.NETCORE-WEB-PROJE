
using Deneme.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Deneme.Models
{
    public class RandevuForm
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }  
        public ApplicationUser UserName { get; set; }
        [ForeignKey("doctorId")]
        public int doctorId { get; set; }
        
        public Doctor doctor { get; set; }
        [ForeignKey("departmentId")]
        public int departmentId { get; set; }
        
        public Department department { get; set; }
        public DateTime appointmentdateTime { get; set; }

    }
}

