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
                        a.*, 
                        v.*
                        FROM vaults v
                        JOIN accounts a ON v.creatorId = a.id
                        WHERE v.id = @id;
            ";
            return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
            {
                vault.Creator = profile;
                return vault; 
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        public Vault Create(Vault newVault)
        {  
            string sql = @"
            INSERT INTO vaults
            (creatorId, name, img, description, isPrivate)
            VALUES
            (@CreatorId, @Name, @Img, @Description, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, newVault);
            return GetById(id);
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