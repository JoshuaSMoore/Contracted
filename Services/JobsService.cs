using System;
using System.Collections.Generic;
using Contracted.Repositories;

namespace Contracted.Services
{
  public class JobsService
{
private readonly JobsRepository _jobsRepository;

public JobsService(JobsRepository jobsRepository)
{
_jobsRepository = jobsRepository;
}

public List<Job> GetAll()
{
return _jobsRepository.Get();
}

public Job GetById(int companyId)
{
Job foundJob = _jobsRepository.Get(companyId);
if(foundJob == null)
{
throw new Exception("Unable to find job");
}
return foundJob;
}

public Job Create(Job jobData)
{
return _jobsRepository.Create(jobData);
}

public void DeleteJob(int jobId, string userId)
{
Job foundJob = GetById(jobId);
if(foundJob.CreatorId != userId)
{
throw new Exception("That aint your job");
}
_jobsRepository.Delete(jobId);
}

}
}