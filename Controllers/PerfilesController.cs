using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly DateTime fechaActual = DateTime.Now;

        public PerfilesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        [Route("perfilDeUsuario/{idPerfil}")]
        public ActionResult perfilDeUsuario(int idPerfil)
        {
            try
            {
                var perfil = (from p in _context.Perfiles
                              join po in _context.PerfilOperacion on p.idPerfil equals po.idPerfil
                              join o in _context.Operaciones on po.idOperacion equals o.idOperacion
                              join m in _context.Modulos on o.idModulo equals m.idModulo
                              where p.idPerfil == idPerfil
                              select new
                              {
                                  p.idPerfil,
                                  p.nombrePerfil,
                                  m.nombreModulo,
                                  o.agregar,
                                  o.leer,
                                  o.editar,
                                  o.eliminar
                              }).ToArray();
                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
