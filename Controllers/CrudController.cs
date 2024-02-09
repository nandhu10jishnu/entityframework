using EmployeeDetails.Interface;
using EmployeeDetails.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IEmploye iemploye;
        public CrudController(IEmploye iemploye)
        {
            this.iemploye = iemploye;
        }
        [HttpGet("GetEmployeesByCompany")]
        public ActionResult<List<Employe>> GetEmployeesByCompany([FromQuery] string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                return BadRequest("Company name cannot be empty");
            }

            
            var employeesInCompany = iemploye.Get().Where(emp => emp.Companyname == companyName).ToList();

            if (employeesInCompany.Count > 0)
            {
                return employeesInCompany;
            }
            else
            {
                return NotFound($"No employees found for company: {companyName}");
            }
        }
        [HttpGet("GetCombinedDataByCompanyName")]
        public async Task<ActionResult<List<EmployeeCompanyData>>> GetCombinedDataByCompanyNameAsync([FromQuery] string companyName)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                return BadRequest("Company name cannot be empty");
            }

            var combinedData = await iemploye.GetCombinedDataByCompanyNameAsync(companyName);

            if (combinedData != null && combinedData.Any())
            {
                return combinedData;
            }
            else
            {
                return NotFound($"No combined data found for company: {companyName}");
            }
        }





        [HttpGet]
        [Route("GetallUser")]
        public ActionResult<List<Employe>> GetallEmploye()
        {
            return iemploye.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<Employe> Get(string id)
        {
            var employe = iemploye.Get(id);
            if (employe == null)
            {
                return NotFound($"Employe with Id = {id}not found");
            }

            return employe;
        }
        [HttpPost]
        public ActionResult<Employe> Post([FromBody]Employe employe)
        {
           iemploye.Create(employe);
            return CreatedAtAction(nameof(Get),new {id= employe.Id },employe);

        }
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Employe employe)
        {
            var existingEmploye = iemploye.Get(id);
            if (existingEmploye == null)
            {
                return NotFound($"Employe with Id ={id}not found");
            }
            iemploye.Update(id, employe);
            return NoContent();

        }
        [HttpDelete("deleteEmploye")]
        public ActionResult Delete(string id)
        {
            var employe = iemploye.Get(id);
            if (employe == null)
            {
                return NotFound($"Employe with Id = {id}not found");
            }
            iemploye.Remove(employe.Id);
            return Ok($"Employe with Id = {id} deleted");
        }
    }
}
