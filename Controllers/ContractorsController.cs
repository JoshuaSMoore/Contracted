using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contracted.Models;
using Contracted.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;

namespace Contracted.Controllers

{
  [ApiController]
[Route("api/[controller]")]
public class ContractorsController : ControllerBase
{
private readonly ContractorsService _contractorsService;
public ContractorsController(ContractorsService contractorsService)
{
_contractorsService = contractorsService;
}
[HttpGet]
public ActionResult<List<Contractor>> GetAll()
{
try
{
return Ok(_contractorsService.GetAll());
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[HttpGet("{contractorId}")]
public ActionResult<Contractor> GetById(int contractorId)
{
try
{
return Ok(_contractorsService.GetById(contractorId));
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[Authorize]
[HttpPost]
public async Task<ActionResult<Contractor>> Post([FromBody] Contractor contractorData)
{
try
{
Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
contractorData.CreatorId = userInfo.Id;
Contractor createdContractor = _contractorsService.Post(contractorData);
createdContractor.Creator = userInfo;
return createdContractor;
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[Authorize]
[HttpDelete("{contractorId}")]
public async Task<ActionResult<string>> RemoveContractor(int contractorId)
{
try
{
Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
_contractorsService.RemoveContractor(contractorId, userInfo.Id);
return Ok("contractor was delorted");
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
}
}