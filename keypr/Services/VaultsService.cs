using System;
using System.Collections.Generic;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }
        
        internal Vault Create(Vault newVault)
        {
           return _repo.Create(newVault);
        }
        internal Vault Get(int id)
        {
            Vault found = _repo.GetById(id);
            if (found == null || found.IsPrivate == true )
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal Vault Edit(Vault updatedVault)
        {
            Vault original = Get(updatedVault.Id);
            if (original.CreatorId != updatedVault.CreatorId)
            {
                throw new Exception("You can't edit someone else's vault");
            }
            original.Name = updatedVault.Name ?? original.Name;
            original.Description = updatedVault.Description ?? original.Description;
            original.IsPrivate = updatedVault.IsPrivate;
            original.Img = updatedVault.Img ?? original.Img;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int vaultId, string userId)
        {
           Vault toDelete = Get(vaultId);
            if (toDelete.CreatorId != userId)
        {
            throw new Exception("Don't delete other people's vaults, jerk");
        }
            _repo.Delete(vaultId);
        }

        internal List<Vault> GetVaultsByProfileId(string creatorId)
        {
          List<Vault> vaults = _repo.GetAll(creatorId);
          return vaults.FindAll(v => v.IsPrivate == false && v.CreatorId == creatorId);
        }
    }
}