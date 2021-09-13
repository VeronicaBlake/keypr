using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keypr.Models;
using keypr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keypr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _service;
        private readonly KeepsService _ks;

        public VaultsController(VaultsService service, KeepsService ks)
        {
            _service = service;
            _ks = ks;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo.Id;
                Vault vault = _service.Create(newVault);
                return Ok(vault);
            }
            catch (Exception err)   
            {
                return BadRequest(err.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _service.Get(id, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Edit(Vault updatedVault, int id)
        {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            updatedVault.CreatorId = userInfo.Id;
            updatedVault.Id = id;
            Vault vault = _service.Edit(updatedVault);
            return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
    }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<String>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _service.Delete(id, userInfo.Id);
                return Ok("Vault Deleted");
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<VaultKeepKeepViewModel>> GetKeeps(int id)
        {
            try
            {
                List<VaultKeepKeepViewModel> keep = _ks.GetVaultKeeps(id);
                return Ok(keep);
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
       
    }
} 