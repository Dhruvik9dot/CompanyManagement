using CompanyManagement.Helpers;
using CompanyManagement.Interface;
using CompanyManagement.Model;
using CompanyManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesInfoController : ControllerBase
    {

        private readonly ICompaniesInfoRepository _companiesInfoRepository;
        public CompaniesInfoController(ICompaniesInfoRepository companiesInfoRepository)
        {
            _companiesInfoRepository = companiesInfoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCompany()
        {
            var result = await _companiesInfoRepository.GetAllCompany();
            
            if (!result.Success)
                return StatusCode(result.Code, result);
            
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCompanyById(Guid id)
        {
            var result = await _companiesInfoRepository.GetCompanyById(id);
            
            if (!result.Success)
                return StatusCode(result.Code, result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddCompany(AddCompanyInfoVM companyInfo)
        {
            var result = await _companiesInfoRepository.AddCompany(companyInfo);

            if (!result.Success)
                return StatusCode(result.Code, result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyInfoVM companyInfo)
        {
            if (id != companyInfo.Id)
                return BadRequest(new BaseResponse{ Success=false,Message = "Company ID mismatch" });

            var result = await _companiesInfoRepository.UpdateCompany(companyInfo);

            if (!result.Success)
                return StatusCode(result.Code, result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var result = await _companiesInfoRepository.DeleteCompany(id);

            if (!result.Success)
                return StatusCode(result.Code, result);

            return Ok(result);
        }
    }
}
