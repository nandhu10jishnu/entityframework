using EmployeeDetails.Interface;
using EmployeeDetails.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany icompany;
        public CompanyController(ICompany icompany)
        {
            this.icompany = icompany;
        }
        [HttpGet]
        [Route("GetallCompany")]
        public ActionResult<List<Company>> GetallCompany()
        {
            return icompany.Get();
        }
        [HttpGet("{id}")]
        public ActionResult<Company> Get(string id)
        {
            var company = icompany.Get(id);
            if (company == null)
            {
                return NotFound($"Company with Id = {id}not found");
            }

            return company;
        }
        [HttpPost]
        public ActionResult<Employe> Post([FromBody] Company company)
        {
            icompany.Create(company);
            return CreatedAtAction(nameof(Get), new { id = company.Id }, company);

        }
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Company company)
        {
            var existingCompany = icompany.Get(id);
            if (existingCompany == null)
            {
                return NotFound($"Company with Id ={id}not found");
            }
            icompany.Update(id, company);
            return NoContent();

        }
        [HttpDelete("deleteiCompany")]
        public ActionResult Delete(string id)
        {
            var company = icompany.Get(id);
            if (company == null)
            {
                return NotFound($"Company with Id = {id}not found");
            }
            icompany.Remove(company.Id);
            return Ok($"Company with Id = {id} deleted");
        }
    }
}
