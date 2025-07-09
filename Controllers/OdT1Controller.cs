using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdT1Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        public OdT1Controller(AppDbContext context)
        {
            this._context = context;
        }
        // GET: api/<CargaExcelController>
        [HttpGet]
        [EnableCors("AllowAnyOrigin")]
        // GET: api/<OdT1Controller>
        public IActionResult Get()
        {
            try
            {
                OdT1Data odT1Data = new OdT1Data(_context);
                var odts = odT1Data.ConsultaOdTs();
                return Ok(new
                {
                    status = "ok",
                    ordenesDetrabajo = odts
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<OdT1Controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OdT1Controller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OdT1Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OdT1Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
