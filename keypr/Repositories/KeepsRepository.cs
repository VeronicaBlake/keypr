using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keypr.Models;

namespace keypr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Keep> GetAll()
        {
            string sql = @"
                SELECT
                a.*,
                k.*
                FROM keeps k
                JOIN accounts a ON a.id = k.creatorId
                ;";
            return _db.Query<Profile, Keep, Keep>(sql, (profile, keep) =>
            {
                keep.Creator = profile;
                return keep;
            }, splitOn: "id").ToList();
        }
        internal Keep Get(int id)
        {
            throw new NotImplementedException();
        }

        internal Keep Create(Keep newKeep)
        {
            throw new NotImplementedException();
        }
    }
}