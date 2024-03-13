using DapperAspNetCore.Contracts;
using DapperAspNetCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperAspNetCore.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepo) => _companyRepo = companyRepo;

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepo.GetCompanies();
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "GetCompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepo.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDto companyDto)
        {
            var company = await _companyRepo.CreateCompany(companyDto);
            return CreatedAtRoute("CompanyNameById", new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyDto companyDto)
        {
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
            {
                return NotFound();
            }
            await _companyRepo.UpdateCompany(id, companyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComapy(int id)
        {
            var dbCompany = await _companyRepo.GetCompany(id);
            if (dbCompany == null)
            {
                return NotFound();
            }
            await _companyRepo.DeleteCompany(id);
            return NoContent();
        }

    }
}
