using InMemoryCaching.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public List<Employee> empData { get; set; }
        public EmployeeController(IMemoryCache cache)
        {
            _cache = cache;
            empData=new List<Employee>()
            {
                new Employee() {Id=1,empName="Robin"},
                new Employee() {Id=2,empName="Clark"},
                new Employee() {Id=3,empName="Michal"},
                new Employee() {Id=4,empName="Stephin"},
            };
        }
        [HttpGet("GetCachedEmployees")]
        public IActionResult GetCachedEmployees()
        {
            Employee employee = new Employee();
            var retCache = _cache.TryGetValue("employeeList", out List<Employee> employees);
            if (!retCache)
            {
                employees = empData;
                _cache.Set("employeeList", employees, TimeSpan.FromMinutes(1));
            }
            return Ok(employees);
        }
    }
}
