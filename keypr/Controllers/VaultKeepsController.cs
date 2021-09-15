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

    public VaultKeepsController(VaultKeepsService vkservice)
    {
      _vkservice = vkservice;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVK.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vkservice.Create(newVK, userInfo.Id);
        return Ok(vaultKeep);
        //TODO how to ensure that the vault creator is the one creating this?
        //find the vault the keep is going to be added to 
        //find the creator of that vault 
        //if that creator is not == userInfo, throw the exception
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