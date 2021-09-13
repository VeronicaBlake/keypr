using System;
using System.Collections.Generic;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
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
            return _repo.GetVaultKeeps(vaultId);
        }
        internal List<VaultKeepKeepViewModel> GetKeepsForAccount(string id)
        {
            throw new NotImplementedException();
        }
    }
}