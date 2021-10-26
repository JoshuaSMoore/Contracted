using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Contracted.Interfaces;
using Dapper;

namespace Contracted.Repositories
{
  public class CompaniesRepository : IRepo<Company>
  {
    private readonly IDbConnection _db;
    public CompaniesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Company Create(Company data)
    {
      string sql = @"
      INSERT INTO companies(
        name,
        creatorId
        )
        VALUES(
          @Name,
          @CreatorId
        );
        SELECT LAST_INSERT_ID();
        ";
        int id = _db.ExecuteScalar<int>(sql, data);
        data.Id = id;
        return data;
    }

    public void Delete(int id)
    {
      string sql = "DELETE FROM companies WHERE id = @id LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new {id});
      if(rowsAffected == 0)
      {
        throw new Exception("Cannot delete this Company");
      }
    }

    public Company Edit(int id, Company data)
    {
      throw new System.NotImplementedException();
    }

    public List<Company> Get()
    {
     string sql = "SELECT * FROM companies";
     return _db.Query<Company>(sql).ToList();
    }

    public Company Get(int id)
    {
      string sql = "SELECT * FROM companies WHERE id = @id";
      return _db.Query<Company>(sql, new {id}).FirstOrDefault();
    }
  }
}