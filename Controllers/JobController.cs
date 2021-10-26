using System.Collections.Generic;
using System.Threading.Tasks;
using Contracted.Models;
using Contracted.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contracted.Controllers

{
  [ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
private readonly JobsService _jobsService;
public JobsController(JobsService jobsService)
{
_jobsService = jobsService;
}
[HttpGet]
public ActionResult<List<Job>> GetAll()
{
try
{
return Ok(_jobsService.GetAll());
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[HttpGet("{jobId}")]
public ActionResult<Job> GetById(int jobId)
{
try
{
return Ok(_jobsService.GetById(jobId));
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[Authorize]
[HttpPost]
public async Task<ActionResult<Job>> Post([FromBody] Job jobData)
{
try
{
Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
// for node reference - req.body.creatorId = req.userInfo.id
// FIXME NEVER TRUST THE CLIENT
jobData.CreatorId = userInfo.Id;
Job createdJob = _jobsService.Create(jobData);
createdJob.Creator = userInfo;
return createdJob;
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
[Authorize]
[HttpDelete("{jobId}")]
public async Task<ActionResult<string>> RemoveJob(int jobId)
{
try
{
Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
_jobsService.DeleteJob(jobId, userInfo.Id);
return Ok("job was delorted");
}
catch (System.Exception e)
{
return BadRequest(e.Message);
}
}
}
}