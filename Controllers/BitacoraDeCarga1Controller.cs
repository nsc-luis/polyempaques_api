using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraDeCarga1Controller : ControllerBase
    {
        private readonly AppDbContext _context;
        public BitacoraDeCarga1Controller(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult Get(int idOdT1)
        {
            try
            {
                BitacoraDeCarga1 bitacoraDeCarga = new BitacoraDeCarga1(_context);
                var registros = bitacoraDeCarga.registrosDeCarga(idOdT1);
                return Ok(new
                {
                    status = "ok",
                    bitacora = registros
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
