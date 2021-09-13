using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keypr.Models;
using keypr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keypr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        // private readonly KeepsService _keepsService;
        // private readonly VaultsService _vaultsService;

        public ProfilesController(ProfilesService profilesService)
        {
            _profilesService = profilesService;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_profilesService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // [HttpGet("vaults")]
        // public async Task<ActionResult<List<VaultKeepVaultViewModel>>> GetVaults()
        // {
        //     try
        //     {
        //         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        //         List<VaultKeepVaultViewModel> vaults = _vaultsService.GetVaultsForAccount(userInfo.Id);
        //         return Ok(vaults);
        //     }
        //     catch (Exception e)
        //     {
        //     return BadRequest(e.Message);
        //     } 
        // }

        // [HttpGet("keeps")]
        // public async Task<ActionResult<List<VaultKeepKeepViewModel>>> GetKeeps()
        // {
        //     try
        //     {
        //         Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        //         List<VaultKeepKeepViewModel> keeps = _keepsService.GetKeepsForAccount(userInfo.Id);
        //         return Ok(keeps);
        //     }
        //     catch (Exception e)
        //     {
        //     return BadRequest(e.Message);
        //     } 
        // }
    }
}
