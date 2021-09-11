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
        public ActionResult<Vault> Get(int id)
        {
            try
            {
                Vault vault = _service.Get(id);
                return Ok(vault);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}

    //TODO: create, edit, delete 
    //REVIEW why ActionResult sometimes? I forget }