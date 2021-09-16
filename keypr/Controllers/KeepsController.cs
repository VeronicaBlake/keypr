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
    public class KeepsController : ControllerBase
    {
         private readonly KeepsService _service;
        private readonly VaultsService _vs;

        public KeepsController(KeepsService service, VaultsService vs)
        {
            _service = service;
            _vs = vs;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newKeep.CreatorId = userInfo.Id;
                Keep keep = _service.Create(newKeep);
                return Ok(keep);
            }
            catch (Exception err)   
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Keep>> Get()
        {
            try
            {
                List<Keep> keeps = _service.Get();
                return Ok(keeps);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
    }
      
        [HttpGet("{id}")]
        public async Task<ActionResult<Keep>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep keep = _service.Get(id);
                return Ok(keep);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep updatedKeep, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedKeep.CreatorId = userInfo.Id;
                updatedKeep.Id = id;
                Keep keep = _service.Edit(updatedKeep);
                return Ok(keep);
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
                return Ok("Keep Deleted");
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}