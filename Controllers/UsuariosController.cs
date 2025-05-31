using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Models;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly DateTime fechaActual = DateTime.Now;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public ActionResult Post()
        {
            try
            {
                var usuarios = _context.Usuarios.ToList();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return Ok (ex.Message);
            }
        }
    }
}
