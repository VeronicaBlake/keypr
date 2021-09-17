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
        internal Vault Get(int id, string userId)
        {
            Vault found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("VaultsService: Invalid Id");
                //TODO idk why, but found is returning as null
            }
            if (found.IsPrivate == true && found.CreatorId != userId)
            {
                throw new Exception("Hands off, buddy");
            }
            return found;
        }



        internal Vault Edit(Vault updatedVault)
        {
            Vault original = Get(updatedVault.Id, updatedVault.CreatorId);
            if (original.CreatorId != updatedVault.CreatorId)
            {
                throw new Exception("You can't edit someone else's vault");
            }
            original.Name = updatedVault.Name ?? original.Name;
            original.Description = updatedVault.Description ?? original.Description;
            original.IsPrivate = updatedVault.IsPrivate ?? original.IsPrivate;
            original.Img = updatedVault.Img ?? original.Img;
            _repo.Edit(original);
            return original;
        }

        internal void Delete(int vaultId, string userId)
        {
           Vault toDelete = Get(vaultId, userId);
            if (toDelete.CreatorId != userId)
        {
            throw new Exception("Don't delete other people's vaults, jerk");
        }
            _repo.Delete(vaultId);
        }

        // internal List<Vault> GetVaultsByProfileId(string profileId)
        // {
        //   List<Vault> vaults = _repo.GetAll(profileId);
        //   return vaults.FindAll(v => v.IsPrivate == false);

        // }

        // internal List<Vault> GetVaultsByAccountId(string userinfoId, bool isPrivate = true)
        // {
        //   List<Vault> vaults = _repo.GetAll(userinfoId);
        //   if(isPrivate)
        //   {
        //     vaults = vaults.FindAll(v => v.IsPrivate == true);
        //   }
        //   return vaults;
        // }


        internal List<Vault> GetVaultsByProfileId(string ownerId, string userId)
        {
          List<Vault> vaults = _repo.GetAll(ownerId);
          if(userId == ownerId)
          {
            return vaults;
          }
          if(userId != ownerId ){
          vaults = vaults.FindAll(v => v.IsPrivate == false);
          }
          return vaults;
        }
    }
}
