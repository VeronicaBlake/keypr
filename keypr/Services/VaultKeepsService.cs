using System;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsRepository _vaultRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vaultRepo)
        {
            _vkRepo = vkRepo;
            _vaultRepo = vaultRepo;
        }

        internal VaultKeep Create(VaultKeep newVK, string userId)
        {
            if (newVK.CreatorId != userId)
            {
                throw new Exception("Don't put things in other people's vaults, dude.");
            }
            return _vkRepo.Create(newVK);
        }
        internal VaultKeep Get(int id)
        {
            VaultKeep found = _vkRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }
        internal void Delete(int vkId, string userId)
        {
           VaultKeep toDelete = Get(vkId);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("Don't move other people's keeps from vaults");
            }
            _vkRepo.Delete(vkId); 
        }
    }
}