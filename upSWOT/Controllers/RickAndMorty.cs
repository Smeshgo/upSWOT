using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace upSWOT.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class RickAndMorty : ControllerBase
    {
        // GET: api/<RickAndMorty>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RickAndMorty>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RickAndMorty>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RickAndMorty>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RickAndMorty>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
