using BlazorLearning.Server.Interfaces;
using BlazorLearning.Shared.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BlazorLearning.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeManager employeeManager, ILogger<EmployeeController> logger)
        {
            _employeeManager = employeeManager;
            _logger = logger;
        }

        [HttpGet("fetchEmployees/{page}/{pageSize}/{sortColumn}/{sortDirection}")]
        public async Task<ActionResult> Employees(int page, int pageSize, string sortColumn, string sortDirection)
        {
            _logger.LogInformation("serilog Employees() called");
            var result = await _employeeManager.GetEmployeeDetails(page, pageSize, sortColumn, sortDirection);
            return Ok(result);
        }

        [HttpGet("fetchEmployeeById/{empId}")]
        public async Task<ActionResult> GetEmployeeById(int empId)
        {
            var result = await _employeeManager.GetEmployeeData(empId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet("{empId}")]
        public async Task<ActionResult> Get(int empId)
        {
            var result = await _employeeManager.GetEmployeeData(empId);
            
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        
        [HttpPost("create")]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeViewModel empDetails)
        {
            if (empDetails == null)
                return BadRequest();
            
            //var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);

            //if (emp != null)
            //{
            //    ModelState.AddModelError("Email", "Employee email already in use");
            //    return BadRequest(ModelState);
            //}
            
            var empResult = await _employeeManager.AddEmployee(empDetails);
            return Ok(empResult);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateEmployee([FromBody] EmployeeViewModel empDetails)
        {
            if (empDetails == null)
                return BadRequest();
            var empResult = await _employeeManager.UpdateEmployeeDetails(empDetails);
            return Ok(empResult);
        }

        [HttpDelete("deleteEmployee/{empId}")]
        public async Task<ActionResult> DeleteEmployee(int empId)
        {

            
            await _employeeManager.DeleteEmployee(empId);
            return Ok();
        }
    }
}
