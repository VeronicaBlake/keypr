using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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
        public async Task<List<Vault>> GetVaultsAsync(string id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                return _vaultsService.GetVaultsByProfileId(id, user?.Id);
            }
            catch (Exception e)
            {
            throw new Exception(e.Message);
            } 
        }
        
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
