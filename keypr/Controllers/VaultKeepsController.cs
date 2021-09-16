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
  [Route("/api/[controller]")]
  [Authorize]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vkservice;
    private readonly VaultsService _vaultsService;

    public VaultKeepsController(VaultKeepsService vkservice, VaultsService vaultsService)
    {
      _vkservice = vkservice;
      _vaultsService = vaultsService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vaultAddingTo = _vaultsService.Get(newVK.VaultId, userInfo.Id);
        if(vaultAddingTo.CreatorId == userInfo.Id){
        newVK.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vkservice.Create(newVK, userInfo.Id);
        return Ok(vaultKeep);
        }
        return BadRequest("Don't add keeps to other people's vaults, dude.");
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
          _vkservice.Delete(id, userInfo.Id);
          return Ok("Keep removed from Vault");
        }
      catch (Exception err)
        {
          return BadRequest(err.Message);
        }
      }

  }
}