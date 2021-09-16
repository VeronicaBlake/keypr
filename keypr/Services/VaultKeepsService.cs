using System;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkRepo;
        private readonly VaultsRepository _vaultRepo;
        private readonly KeepsRepository _keepRepo;

        public VaultKeepsService(VaultKeepsRepository vkRepo, VaultsRepository vaultRepo, KeepsRepository keepRepo)
        {
            _vkRepo = vkRepo;
            _vaultRepo = vaultRepo;
            _keepRepo = keepRepo;
        }

        internal VaultKeep Create(VaultKeep newVK, string userId)
        {
            if (newVK.CreatorId != userId)
            {
                throw new Exception("Don't put things in other people's vaults, dude.");
            }
            var vk =  _vkRepo.Create(newVK);
            //TODO call keep repo and incriment keeps
            _keepRepo.changeKeeps(newVK.KeepId, 1);
            return vk;
        }
        internal VaultKeep Get(int id, string userId=null)
        {
            VaultKeep found = _vkRepo.GetById(id);
            Vault foundVault = _vaultRepo.GetById(found.VaultId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            if(foundVault.IsPrivate == false)
            {
            return found;
            }
            if(foundVault.IsPrivate == true){
                if(foundVault.CreatorId == userId){
                    return found;
                }
                throw new Exception("VK Service: this one might be a little janky This is a Private Vault.");
            }
            throw new Exception("VK Service: this one might be a little janky This is a Private Vault.");
        }
        internal void Delete(int vkId, string userId)
        {
           VaultKeep toDelete = Get(vkId);
            if (toDelete.CreatorId != userId)
            {
                throw new Exception("Don't move other people's keeps from vaults");
            }
             _keepRepo.changeKeeps(toDelete.KeepId, -1);
            _vkRepo.Delete(vkId); 
        }
    }
}