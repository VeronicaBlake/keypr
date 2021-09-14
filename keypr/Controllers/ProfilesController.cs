using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using keypr.Models;
using keypr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keypr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]    
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
        {
            _accountService = accountService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

      [HttpGet("{id}")]
       public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _accountService.GetProfileById(id);
                return profile;
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        
        [HttpGet("{id}/vaults")]
        public Task<List<Vault>> GetVaultsAsync(string id)
        {
            try
            {
                Profile userInfo = _accountService.GetProfileById(id);
                List<Vault> vaults = _vaultsService.GetVaultsByAccountId(userInfo.Id);
                return Task.FromResult(vaults);
            }
            catch (Exception e)
            {
            throw new Exception(e.Message);
            } 
        }
        
        // [HttpGet("{id}/vaults")]
        // public async Task<List<Vault>> GetVaultsAsync(string id)
        // {
        //     try
        //     {
        //         Profile userInfo = _accountService.GetProfileById(id);
        //         Account privateVaultOwner = await HttpContext.GetUserInfoAsync<Account>();
        //         if(userInfo.Id != privateVaultOwner.Id){
        //             List<Vault> profileVaults = _vaultsService.GetVaultsByProfileId(id);
        //             return profileVaults;
        //         }
        //             List<Vault> vaults = _vaultsService.GetVaultsByAccountId(id);
        //             return vaults;
        //     }
        //     catch (Exception e)
        //     {
        //     throw new Exception(e.Message);
        //     } 
        // }
        
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetVaultsByProfileId(string id)
        {
           try
            {
                List<Keep> keeps = _keepsService.GetKeepsByProfileId(id);
                return keeps;
            }
           catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}
