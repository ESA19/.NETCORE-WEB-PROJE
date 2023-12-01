
namespace Deneme.Models
{
    public class Department
    {
        public int Id { get; set; }   
        public string DepartmentName { get; set; }
        public List<Doctor> doctors { get; set; }
    }
}
