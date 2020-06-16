using System;
using Microsoft.AspNetCore.Mvc;
using Test2_Example.Services;

namespace Test2_Example.Controllers
{
    [ApiController]
    [Route("/api/music-labels")]
    public class LabelController : ControllerBase
    {
        private readonly IMusicDbService _service;

        public LabelController(IMusicDbService service)
        {
            _service = service;
        }

        [HttpGet("{id?}")]
        public IActionResult GetLabelInfo(int id)
        {
            try
            {
                var result = _service.GetLabelInfo(id);
                return Ok(result);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        


    }
}