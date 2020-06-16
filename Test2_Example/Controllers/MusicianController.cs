using System;
using Microsoft.AspNetCore.Mvc;
using Test2_Example.Services;

namespace Test2_Example.Controllers
{
    [ApiController]
    [Route("api/musicians")]
    public class MusicianController : ControllerBase
    {
        private readonly IMusicDbService _service;

        public MusicianController(IMusicDbService service)
        {
            _service = service;
        }

        [HttpDelete("{id?}")]
        public IActionResult DeleteMusician(int id)
        {
            try
            {
                var result = _service.DeleteMusician(id);
                return Ok(result);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}