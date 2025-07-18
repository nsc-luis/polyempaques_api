﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Polyempaques_API.Data;
using Polyempaques_API.Models;
using Spire.Doc;
using static Polyempaques_API.Data.DocumentoPDF;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Polyempaques_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargaExcelController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CargaExcelController(AppDbContext context)
        {
            this._context = context;
        }
        // GET: api/<CargaExcelController>
        [HttpPost]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult Post()
        {
            //try
            //{
            //    CargaExcel cargaExcel = new CargaExcel(_context);
            //    DocumentoPDFGenerado prueba = cargaExcel.ExcelData(path);
            //    DocumentoPDFGenerado documentoPDFGenerado = cargaExcel.ExcelData(path);
            //    Spire.Doc.Document spireDoc = documentoPDFGenerado.documento;

            //    spireDoc.SaveToFile(documentoPDFGenerado.archivoDeSalida, FileFormat.PDF);
            //    System.IO.File.Delete(documentoPDFGenerado.archivoTemporal);
            //    byte[] FileByteData = System.IO.File.ReadAllBytes(documentoPDFGenerado.archivoDeSalida);
            //    return File(FileByteData, "application/pdf");
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            try
            {
                if (Request.Form.Files.Count > 0){
                    string fullPath = string.Empty;
                    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                    if (!Directory.Exists(pathToSave))
                        Directory.CreateDirectory(pathToSave);
                    foreach (IFormFile file in Request.Form.Files)
                    {
                        fullPath = Path.Combine(pathToSave, file.FileName);
                        using FileStream stream = new(fullPath, FileMode.Create);
                        file.CopyTo(stream);
                    }
                    CargaExcel cargaExcel = new CargaExcel(_context);
                    return Ok(cargaExcel.ExcelData2(fullPath));
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult Get(string path)
        {
            try
            {
                CargaExcel cargaExcel = new CargaExcel(_context);
                return Ok(cargaExcel.ExcelData2(path));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
