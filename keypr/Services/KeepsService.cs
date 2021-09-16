using System;
using System.Collections.Generic;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;

        private readonly VaultsRepository _vaultRepo;

        public KeepsService(KeepsRepository repo,  VaultsRepository vaultRepo)
        {
            _repo = repo;
             _vaultRepo = vaultRepo;
        }
        internal Keep Create(Keep newKeep)
        {
           return _repo.Create(newKeep);
        }

        internal List<Keep> Get()
        {
         return _repo.GetAll();  
        }

        internal Keep Get(int id)
        {
            Keep keep = _repo.GetById(id);
            if (keep == null)
            {
                throw new Exception("Invalid Keep Id");
            }
            return keep;
        }

        internal Keep Edit(Keep updatedKeep)
        {
            Keep originalKeep = Get(updatedKeep.Id);
            if (originalKeep.CreatorId != updatedKeep.CreatorId)
            {
                throw new Exception("Hands off Buddy");
            }
            originalKeep.Name = updatedKeep.Name ?? originalKeep.Name;
            originalKeep.Description = updatedKeep.Description ?? originalKeep.Description;
            originalKeep.Img = updatedKeep.Img ?? originalKeep.Img;
            _repo.Edit(originalKeep);
            return originalKeep;
        }

        internal void Delete(int keepId, string userId)
        {
            Keep toDelete = Get(keepId);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("Don't delete other people's keeps");
            }
            _repo.Delete(keepId);
        }
        internal List<VaultKeepKeepViewModel> GetVaultKeeps(int vaultId)
        {
            Vault foundVault = _vaultRepo.GetById(vaultId);
            if(foundVault.IsPrivate == true)
            {
             throw new Exception("This vault is private.");
            }
            return _repo.GetVaultKeeps(vaultId);
        }

          // internal List<VaultKeepKeepViewModel> GetVaultKeeps(int id)
        // {
        //     VaultKeep found = _vkRepo.GetById(id);
            // Vault foundVault = _vaultRepo.GetById(found.VaultId);
            // List<VaultKeepKeepViewModel> keeps = _repo.GetVaultKeeps(found.VaultId);
            // if (found == null)
            // {
            //     throw new Exception("Invalid Id");
            // }
            // if(foundVault.IsPrivate == false)
            // {
            //  throw new Exception("Get outta here with that.");
            // }
        //     return keeps;
        // }

        internal List<Keep> GetKeepsByProfileId(string profileId)
        {
            List<Keep> keeps = _repo.GetKeepsByProfileId(profileId);
          return keeps;
        }
    }
}