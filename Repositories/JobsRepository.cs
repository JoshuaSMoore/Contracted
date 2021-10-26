using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Contracted.Repositories
{
  public class JobsRepository
  {
    private readonly IDbConnection _db;
    public JobsRepository(IDbConnection db)
    {
      _db = db;
    }
    public Job Create(Job data)
    {
      string sql = @"
      INSERT INTO jobs(contractorId, companyId)
      VALUES(@ContractorId, @CompanyId);
      SELECT LAST_INSERT_ID();";
      data.Id = _db.ExecuteScalar<int>(sql, data);
      return data;
    }

    internal Job Get(object companyId)
    {
      throw new NotImplementedException();
    }

    public List<Job> Get()
    {
     string sql = "SELECT * FROM jobs";
     return _db.Query<Job>(sql).ToList();
    }

    internal void Delete(int jobId)
    {
      throw new NotImplementedException();
    }
  }
}