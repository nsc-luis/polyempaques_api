using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
                qR.timestamp = fechaActual;
                qR.idUsuario = 1;
                qR.activo = true;
                qR.ediciones = 0;
                _context.QR.Add(qR);
                _context.SaveChanges();
                return Ok(new
                {
                    status = "success"
                });
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
            var qrSeek = _context.QR.FirstOrDefault(qr => qr.idQR == qR.idQR);
            int? ediciones = qrSeek.ediciones == null || qrSeek.ediciones == 0 ? 1 : qrSeek.ediciones + 1;
            try
            {
                qrSeek.descripcion = qR.descripcion;
                qrSeek.partNumber = qR.partNumber;
                qrSeek.quantity = qR.quantity;
                qrSeek.poNumber = qR.poNumber;
                qrSeek.serialNumber = qR.serialNumber;
                qrSeek.timestamp = fechaActual;
                qrSeek.ediciones = ediciones;
                _context.Update(qrSeek);
                _context.SaveChanges();
                return Ok(new
                {
                    status = "success",
                    qrSeek
                });
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
                var qr = _context.QR.FirstOrDefault(qr => qr.idQR == idQR);
                if (qr != null)
                {
                    qr.activo = false;
                    qr.ediciones = qr.ediciones + 1;
                    _context.QR.Update(qr);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
