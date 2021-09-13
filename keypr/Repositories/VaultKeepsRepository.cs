using System.Data;
using Dapper;
using keypr.Models;

namespace keypr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVK)
        {
            string sql = "INSERT INTO vaultkeeps (keepId, vaultId) VALUES (@KeepId, @VaultId); SELECT LAST_INSERT_ID();";
            newVK.Id = _db.ExecuteScalar<int>(sql, newVK); 
            return newVK;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new{id});
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new {id});
        }
    }
}