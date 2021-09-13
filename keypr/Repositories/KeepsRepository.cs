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
        public List<Keep> GetAll()
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
        public Keep GetById(int id)
        {
            string sql = @"
            SELECT 
            a.*, 
            k.*
            FROM keeps k 
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id;";
            return _db.Query<Profile, Keep, Keep>(sql, (profile, keep)=>
            {
                keep.Creator = profile; 
                return keep;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        public Keep Edit(Keep updatedKeep)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description
            WHERE id = @Id
            ;";
            _db.Execute(sql, updatedKeep);
            return updatedKeep;
        }
        public Keep Create(Keep newKeep)
        {
            string sql = @"
            INSERT INTO keeps
            (creatorId, name, description, img, views, shares, keeps)
            VALUES
            (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID();
            ";
            newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
            return GetById(newKeep.Id);
        }

        internal List<Keep> GetKeepsByProfileId(string creatorId)
        {
            string sql = @"
            SELECT
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.creatorId = @creatorid
            ";
            return _db.Query<Profile, Keep, Keep>(sql, (prof, keep) =>
            {
                keep.Creator = prof;
                return keep;
            }, new { creatorId }, splitOn: "id").ToList<Keep>();
        }

        internal List<VaultKeepKeepViewModel> GetVaultKeeps(int vaultId)
        {
            string sql = @"
            SELECT 
            k.*,
            vk.id AS vaultKeepId
            FROM vaultkeeps vk
            JOIN keeps k ON vk.keepId = k.id
            WHERE vk.vaultId = @vaultId;
            ";
            return _db.Query<VaultKeepKeepViewModel>(sql, new {vaultId}).ToList();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}