using System.Data;
using System.Linq;
using Dapper;
using keypr.Models;

namespace keypr.Repositories
{
    public class VaultsRepository 
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        public Vault GetById(int id)
        {
            string sql = @"
                        SELECT 
                        v.*, 
                        a.*
                        FROM vaults v
                        JOIN accounts a ON v.creatorId = a.id
                        WHERE v.id = @id
            ";
            return _db.Query<Vault, Profile, Vault>(sql, (v,p) =>
            {
                v.Creator = p;
                return v; 
            }, new { id }).FirstOrDefault();
        }

        internal Vault Create(Vault newVault)
        {  
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, img, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Img, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
            return newVault;
            //TODO need to populate the creator, how do I do that again?
        }

        public Vault Edit(Vault updatedVault)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id
            ;";
            _db.Execute(sql, updatedVault);
            return updatedVault;
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}