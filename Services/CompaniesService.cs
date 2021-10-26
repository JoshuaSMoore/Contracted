using System.Collections.Generic;
using Contracted.Repositories;

namespace Contracted.Services
{
  public class CompaniesService
  {
    private readonly CompaniesRepository _companiesRepository;

    public CompaniesService(CompaniesRepository companiesRepository)
    {
      _companiesRepository = companiesRepository;
    }
    public List<Company> GetCompanies()
    {
      return _companiesRepository.Get();
    }
    public Company GetById(int companyId)
    {
      Company foundCompany = _companiesRepository.Get(companyId);
      if(foundCompany == null)
      {
        throw new System.Exception("Cannot find this company");
      }
      return foundCompany;
    }
    public Company CreateCompany(Company companyData)
    {
      return _companiesRepository.Create(companyData);
    }
    public void Delete(int companyId, string userId)
    {
      Company company = GetById(companyId);
      if(company.CreatorId != userId)
      {
        throw new System.Exception("Not Authorized Dummy");
      }
      _companiesRepository.Delete(companyId);
    }
  }
}