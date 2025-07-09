using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosOdT1Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public MovimientosOdT1Controller(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet("{idOdT}")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult Get(int idOdT)
        {
            try
            {
                MovimientosOdT1Data movimientosOdT1Data = new MovimientosOdT1Data(_context);
                var movimientos = movimientosOdT1Data.movtosPorOdT(idOdT);
                return Ok(new
                {
                    status = "ok",
                    movimientos
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
