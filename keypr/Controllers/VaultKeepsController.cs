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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVK)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVK.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vkservice.Create(newVK, userInfo.Id);
        return Ok(vaultKeep);
        //TODO how to ensure that the vault creator is the one creating this?
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<String>> Delete(int vkId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string resultStr = _vkservice.Delete(vkId, userInfo.Id);
        return Ok(resultStr);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }

  }
}