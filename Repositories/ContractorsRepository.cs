using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Contracted.Interfaces;
using Dapper;

namespace Contracted.Repositories
{
  public class ContractorsRepository : IRepo<Contractor>
  {
    private readonly IDbConnection _db;
    public ContractorsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Contractor Create(Contractor data)
    {
      string sql = @"
      INSERT INTO contractors(
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
      string sql = "DELETE FROM contractors WHERE id = @id LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new {id});
      if(rowsAffected == 0)
      {
        throw new Exception("Cannot delete this Contractors");
      }
    }

    public Contractor Edit(int id, Contractor data)
    {
      throw new System.NotImplementedException();
    }

  public List<Contractor> Get()
    {
     string sql = "SELECT * FROM contractors";
     return _db.Query<Contractor>(sql).ToList();
    }

    public Contractor Get(int id)
    {
      string sql = "SELECT * FROM contractors WHERE id = @id";
      return _db.Query<Contractor>(sql, new {id}).FirstOrDefault();
    }
  }
}
