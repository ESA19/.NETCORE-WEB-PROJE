
namespace Deneme.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set;}
        public int departmentId { get; set; }   
        public Department department { get; set; }  
        public List<RandevuForm> randevular { get; set; }
    }
}
