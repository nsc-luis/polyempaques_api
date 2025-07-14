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

        [HttpDelete("{idOdT}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult Delete(int idOdT)
        {
            try
            {
                OdT1Data odT1Data = new OdT1Data(_context);
                odT1Data.eliminarOdT(idOdT);
                return Ok(new
                {
                    status = "ok"
                });
            }
            catch (Exception err)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = err.Message
                });
            }
        }
    }
}
