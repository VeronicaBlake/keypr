using System;
using System.Threading.Tasks;
using keypr.Models;
using keypr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace keypr.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly VaultsService _vaultsService;

        public AccountController(AccountService accountService, VaultsService vaultsService)
        {
            _accountService = accountService;
            _vaultsService = vaultsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

         [HttpGet("{id}/vaults")]
         [Authorize]
        public ActionResult<List<Vault>> GetMyVaults(string id)
        {
            try
            {
                List<Vault> vaults = _vaultsService.GetVaultsByAccountId(id);
                return vaults;
            }
            catch (Exception e)
            {
            return BadRequest(e.Message);
            } 
        }
    }
}