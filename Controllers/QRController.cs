using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;
using Polyempaques_API.Models;
using System.Diagnostics;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : Controller
    {
        private readonly AppDbContext _context;

        private readonly DateTime fechaActual = DateTime.Now;

        public QRController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [EnableCors("AllowAnyOrigin")]
        public ActionResult Get()
        {
            try
            {
                var qrs = _context.QR.Where(qr => qr.activo == true).ToList();
                return Ok(qrs);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public ActionResult Post([FromBody] QR qR)
        {
            try
            {
                var qrs = _context.QR.ToList();
                int actualizaciones = qrs.Sum(qr => qr.ediciones ?? 0);
                if ((actualizaciones + qrs.Count) >= 160)
                {
                    return Ok(new
                    {
                        status = "error",
                        mensaje = $"Oops:\nHas llegado al limite de {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                else if ((actualizaciones + qrs.Count) > 150)
                {
                    QRData qRData = new QRData(_context);
                    qRData.Alta(qR);
                    return Ok(new
                    {
                        status = "warning",
                        mensaje = $"Advertencia:\nLlevas {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                
                else
                {
                    QRData qRData = new QRData(_context);
                    qRData.Alta(qR);
                    return Ok(new
                    {
                        status = "success"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = ex.Message
                });
            }
        }

        [HttpPut]
        [EnableCors("AllowAnyOrigin")]
        public ActionResult Put([FromBody] QR qR)
        {
            try
            {
                var qrs = _context.QR.ToList();
                int actualizaciones = qrs.Sum(qr => qr.ediciones ?? 0);
                if (actualizaciones >= 160)
                {
                    return Ok(new
                    {
                        status = "error",
                        mensaje = $"Oops:\nHas llegado al limite de {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                else if (actualizaciones > 150)
                {
                    QRData qRData = new QRData(_context);
                    qRData.Edicion(qR);
                    return Ok(new
                    {
                        status = "warning",
                        mensaje = $"Advertencia:\nLlevas {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                else
                {
                    QRData qRData = new QRData(_context);
                    qRData.Edicion(qR);
                    return Ok(new
                    {
                        status = "success"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = " error",
                    mensaje = ex.Message
                });
            }
        }

        [HttpDelete("{idQR}")]
        [EnableCors("AllowAnyOrigin")]
        public ActionResult Delete(int idQR)
        {
            try
            {
                var qrs = _context.QR.ToList();
                int actualizaciones = qrs.Sum(qr => qr.ediciones ?? 0);
                if (actualizaciones >= 160)
                {
                    return Ok(new
                    {
                        status = "error",
                        mensaje = $"Oops:\nHas llegado al limite de {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                else if(actualizaciones > 150)
                {
                    QRData qRData = new QRData(_context);
                    qRData.Eliminacion(idQR);
                    return Ok(new
                    {
                        status = "warning",
                        mensaje = $"Advertencia:\nLlevas {actualizaciones}/160 alta/actualizacion/eliminación de registros."
                    });
                }
                else
                {
                    QRData qRData = new QRData(_context);
                    qRData.Eliminacion(idQR);
                    return Ok(new
                    {
                        status = "success"
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = ex.Message
                });
            }
        }
    }
}
