using Deneme.Data;
using Deneme.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Deneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentApiController : ControllerBase
    {

        private readonly DenemeDbContext _context;



        public DepartmentApiController(DenemeDbContext context)
        {
            _context = context;


        }
        // GET: api/<DepartmentApiController>
        [HttpGet]
        public List<Department>Get()
        {
            var departments = _context.Departments.ToList();
            return departments;
        }

        // GET api/<DepartmentApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DepartmentApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DepartmentApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DepartmentApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
