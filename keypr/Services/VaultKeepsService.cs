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

        private VaultKeep GetById(int id)
        {
            VaultKeep found = _vkRepo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal string Delete(int id, string accountId)
        {
            VaultKeep vk = GetById(id);
            if (vk.CreatorId == accountId)
            {
                _vkRepo.Delete(id);
                return "Removed keep from vault";
            }
            Vault v = _vaultRepo.GetById(vk.VaultId);
            if (v.CreatorId == accountId)
            {
                _vkRepo.Delete(id);
                return "Keep removed from vault";
            }
            throw new Exception("Invalid Access to this vault's keeps");
        }
    }
}