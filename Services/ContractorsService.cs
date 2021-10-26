using System;
using System.Collections.Generic;
using Contracted.Repositories;

namespace Contracted.Services
{
  public class ContractorsService
{
private readonly ContractorsRepository _contractorsRepository;

public ContractorsService(ContractorsRepository contractorsRepository)
{
_contractorsRepository = contractorsRepository;
}

public List<Contractor> GetAll()
{
return _contractorsRepository.Get();
}

public Contractor GetById(int contractorId)
{
Contractor contractor = _contractorsRepository.Get(contractorId);
if(contractor == null)
{
throw new Exception("Unable to find Contractors");
}
return contractor;
}

public Contractor Post(Contractor contractorData)
{
return _contractorsRepository.Create(contractorData);
}

public void RemoveContractor(int contractorId, string userId)
{
Contractor foundContractor = GetById(contractorId);
if(foundContractor.CreatorId != userId)
{
throw new Exception("That aint your Contractor");
}
_contractorsRepository.Delete(contractorId);
}

}
}