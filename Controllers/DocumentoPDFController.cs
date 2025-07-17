using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;
using Polyempaques_API.Models;
using Spire.Doc;
using static Polyempaques_API.Data.DocumentoPDF;

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentoPDFController : Controller
    {
        private readonly AppDbContext _context;
        public DocumentoPDFController(AppDbContext context)
        {
            this._context = context;
        }

        public class OdTFromBody
        {
            public int idOdT { get; set; }
        }

        [HttpPost]
        [Route("[action]")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GeneraEtiqueta([FromBody] QR qr)
        {
            try
            {
                DocumentoPDF documentoPDF = new DocumentoPDF(_context);
                DocumentoPDFGenerado documentoPDFGenerado = documentoPDF.GeneraEtiqueta(qr);
                Spire.Doc.Document spireDoc = documentoPDFGenerado.documento;

                spireDoc.SaveToFile(documentoPDFGenerado.archivoDeSalida, FileFormat.PDF);
                System.IO.File.Delete(documentoPDFGenerado.archivoTemporal);
                byte[] FileByteData = System.IO.File.ReadAllBytes(documentoPDFGenerado.archivoDeSalida);
                return File(FileByteData, "application/pdf");
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = ex
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult ListaPdfOdT([FromBody] OdTFromBody odt)
        {
            try
            {
                DocumentoPDF documentoPDF = new DocumentoPDF(_context);
                List<string> listaArchivos = documentoPDF.EtiquetasDeUnaOdT(odt.idOdT);
                return Ok(new
                {
                    status = "ok",
                    listaArchivos
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = ex
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult EtiquetasDeLaOdt([FromBody] string file)
        {
            try
            {
                byte[] FileByteData = System.IO.File.ReadAllBytes(file);
                return File(FileByteData, "application/pdf");
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    status = "error",
                    mensaje = ex
                });
            }
        }
    }
}
