using System.Data;
using System.Linq;
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
            string sql = "INSERT INTO vaultkeeps (keepId, vaultId, creatorId) VALUES (@KeepId, @VaultId, @creatorId); SELECT LAST_INSERT_ID();";
            newVK.Id = _db.ExecuteScalar<int>(sql, newVK); 
            return newVK;
        }

        internal VaultKeep GetById(int id)
        {
            string sql = @"
            SELECT 
            a.*,
            vk.*
            FROM vaultkeeps vk
            JOIN accounts a ON vk.creatorId = a.id
            WHERE vk.id = @id;";
            return _db.Query<Profile, VaultKeep, VaultKeep>(sql, (profile, vaultKeep)=>
            {
                vaultKeep.CreatorId = profile.Id; 
                return vaultKeep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}