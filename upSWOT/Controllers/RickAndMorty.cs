using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using upSWOT.Models;
using upSWOT.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace upSWOT.Controllers
{
    [Route("/api/v1/")]
    [ApiController]
    public class RickAndMortyController : ControllerBase
    {
        private readonly IRickAndMortyService _rickAndMortyService;

        public RickAndMortyController(IRickAndMortyService rickAndMortyService)
        {
            _rickAndMortyService = rickAndMortyService;
        }

        [HttpPost("check-person")]
        public async Task<IActionResult> CheckPerson([FromBody] ResponseCheckPerson request)
        {
            var result = await _rickAndMortyService.IsPresent(request);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("person")]
        public async Task<IActionResult> GetHeroByName([FromQuery] string name)
        {
            var result = await _rickAndMortyService.GetHeroByName(name);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
