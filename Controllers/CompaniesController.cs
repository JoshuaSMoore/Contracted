using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Contracted.Models;
using Contracted.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contracted.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
  private readonly CompaniesService _companiesService;

  public CompaniesController(CompaniesService companiesService)
  {
    _companiesService = companiesService;
  }
  [HttpGet]
  public ActionResult<List<Company>> GetCompanies()
  {
    try
    {
         var companies = _companiesService.GetCompanies();
         return Ok(companies);
    }
    catch (System.Exception e)
    {
    return BadRequest(e.Message);
    }
  }
  [HttpGet("{companyId}")]
  public ActionResult<Company> GetById(int companyId)
  {
    try
    {
         var company = _companiesService.GetById(companyId);
         return Ok(company);
    }
    catch (System.Exception e)
    {
        return BadRequest(e.Message);
    }
  }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Company>> CreateCompany([FromBody] Company companyData)
  {
    try
    {
         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
         companyData.CreatorId = userInfo.Id;
         Company company = _companiesService.CreateCompany(companyData);
         company.Creator = userInfo;
         return Ok(company);
    }
    catch (System.Exception e)
    {
     return BadRequest(e.Message);
    }
  }
  [HttpDelete("{companyId}")]
  [Authorize]
public async Task<ActionResult<string>> Delete(int companyId)
{
  try
  {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       _companiesService.Delete(companyId, userInfo.Id);
       return Ok("delorted company");
  }
  catch (System.Exception e)
  {
    return BadRequest(e.Message);
  }
}
}
}