using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using IronXL;
using Polyempaques_API.Models;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static Polyempaques_API.Data.DocumentoPDF;

namespace Polyempaques_API.Data
{
    public class CargaExcel
    {
        private readonly AppDbContext _context;

        public CargaExcel(AppDbContext context)
        {
            this._context = context;
        }

        private string ReadExcelCell(DocumentFormat.OpenXml.Spreadsheet.Cell cell, WorkbookPart workbookPart)
        {
            var cellValue = cell.CellValue;
            var text = (cellValue == null) ? cell.InnerText : cellValue.Text;
            if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
            {
                text = workbookPart.SharedStringTablePart.SharedStringTable
                    .Elements<SharedStringItem>().ElementAt(
                        Convert.ToInt32(cell.CellValue.Text)).InnerText;
            }
            return (text ?? string.Empty).Trim();
        }

        public class DataExcel
        {
            public string celda { get; set; }
            public string valor { get; set; }
        }
        public class ResultadoLecturaExcel
        {
            public string poNumber { get; set; }
            public string partNumber { get; set; }
            public int surtido { get; set; }
            public int totalKgOT { get; set; }
            public int porSurtir { get; set; }
            public bool error { get; set; }
            public List<string> mensaje { get; set; }
            public List<DataExcel> dataExcel { get; set; }
        }
        public DocumentoPDFGenerado ExcelData(string path)
        {
            WorkBook workbook = WorkBook.Load(path);
            WorkSheet sheet = workbook.WorkSheets.First();

            int contador = 8 - sheet["A15"].StringValue.Length;
            string ceros = "";
            for (int i = 0; i < contador; i++)
            {
                ceros = ceros + "0";
            }
            ;

            DocumentoPDF documentoPDF = new DocumentoPDF(_context);
            DocumentoPDFGenerado documentoPDFGenerado = documentoPDF.GeneraEtiqueta(new QR
            {
                idQR = 0,
                descripcion = $"{sheet["G1"].StringValue} {sheet["G2"].StringValue} {sheet["G6"].StringValue} {sheet["G7"].StringValue}",
                partNumber = sheet["G5"].StringValue,
                quantity = sheet["B15"].IntValue,
                poNumber = sheet["G4"].StringValue,
                trace = "",
                serialNumber = $"DJ{ceros}{sheet["A15"].StringValue}"
            });

            return new DocumentoPDFGenerado
            {
                documento = documentoPDFGenerado.documento,
                archivoDeSalida = documentoPDFGenerado.archivoDeSalida,
                archivoTemporal = documentoPDFGenerado.archivoTemporal
            };
        }
        public ResultadoLecturaExcel ExcelData2(string path)
        {
            List<DataExcel> data = new List<DataExcel>();
            bool error = false;
            int number;
            List<string> mensaje = [];

            ResultadoLecturaExcel resultadoLecturaExcel = new ResultadoLecturaExcel();

            using (var ssd = SpreadsheetDocument.Open(path, false))
            {
                var workbookPart = ssd.WorkbookPart;
                var worksheetPart = workbookPart.WorksheetParts.First();
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                string[] celdaNumerica =
                    [
                        "A2","A3","A4","A5","A6","A7","A8","A9","A10","A11","A12","A13","A14","A15","A16","A17","A18","A19","A20","A21",
                        "B2","B3","B4","B5","B6","B7","B8","B9","B10","B11","B12","B13","B14","B15","B16","B17","B18","B19","B20","B21",
                        "G4"
                    ];
                //string[] celdaConContenido = ["G1", "G2", "G3"];
                var rows = sheetData.Descendants<Row>();
                foreach (var row in rows)
                {
                    foreach (var cell in row.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                    {
                        var cellValue = string.Empty;
                        cellValue = ReadExcelCell(cell, workbookPart);
                        data.Add(new DataExcel { celda = cell.CellReference, valor = cellValue });
                    }
                }
                foreach (DataExcel d in data)
                {
                    if (celdaNumerica.Any(d.celda.Contains))
                    {
                        if (!int.TryParse(d.valor, out number))
                        {
                            error = true;
                            mensaje.Add($"La celda {d.celda} debe contener un numero entero.");
                        }
                        else if (Regex.IsMatch(d.celda, "^A\\d"))
                        {
                            int ceros = 8 - d.valor.ToCharArray().Count();
                            string serie = "DJ" + d.valor.PadLeft(ceros + d.valor.ToCharArray().Count(), '0');
                            d.valor = serie;
                        }
                    }
                    //if (celdaConContenido.Any(d.celda.Contains))
                    //{
                    //    if (d.valor == null || d.valor == "")
                    //    {
                    //        error = true;
                    //        mensaje.Add($"Las celdas G1, G2 Y G3 no pueden estar vacias.");
                    //    }
                    //}
                }
                resultadoLecturaExcel = (new ResultadoLecturaExcel
                {
                    error = error,
                    mensaje = mensaje,
                    dataExcel = data
                });
            }

            string mesajeBitacora = "";
            string mesajeBitProducto = "";
            string descripcion = "";
            string poNumber = "";
            string partNumber = "";
            int totalKgOT = 0;

            for (int i = 1; i < 5; i++)
            {
                if (resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}") == null)
                {
                    resultadoLecturaExcel.error = true;
                    mensaje.Add($"La celda G{i} no puede estar vacia.");
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            descripcion = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor == null ? "Mensaje error" : resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor;
                            break;
                        case 2:
                            poNumber = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor == null ? "Mensaje error" : resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor;
                            break;
                        case 3:
                            partNumber = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor == null ? "Mensaje error" : resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor;
                            break;
                        case 4:
                            totalKgOT = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor == null ? 0 : int.Parse(resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"G{i}").valor);
                            break;
                    }
                }
            }

            Producto1 producto1 = new Producto1();
            OdT1 odT1 = new OdT1();
            BitacoraDeCarga1 bitacoraDeCarga1 = new BitacoraDeCarga1(_context);

            if (!resultadoLecturaExcel.error)
            {
                Producto1 producto = _context.Producto1.FirstOrDefault(p => p.partNumber == partNumber);
                if (producto == null)
                {
                    producto1 = new Producto1
                    {
                        descripcion = descripcion,
                        partNumber = partNumber,
                        idUsuario = 1, // Asignar un idUsuario válido   
                        timestamp = DateTime.Now
                    };
                    _context.Producto1.Add(producto1);
                    _context.SaveChanges();
                    mesajeBitProducto = $"El producto con partNumber = {producto1.partNumber}, " +
                        $"se agrego correctamente.";
                }
                else
                {
                    mesajeBitProducto = $"Ya existe un producto con partNumber = {producto.partNumber}, " +
                        $"no pueden existir 2 productos con el mismo partNumber.";
                    producto1 = producto;
                    resultadoLecturaExcel.mensaje.Add
                    (
                        mesajeBitProducto
                    );
                }

                OdT1 odT = _context.OdT1.FirstOrDefault(o => o.poNumber == poNumber && o.idProducto == producto1.idProducto);

                if (odT == null)
                {
                    // Crear una nueva orden de trabajo si no existe una con el mismo poNumber y producto
                    //string descripcion = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == "G1").valor;
                    odT1 = new OdT1
                    {
                        idProducto = producto1.idProducto,
                        poNumber = poNumber,
                        totalKgOT = totalKgOT,
                        idUsuario = 1, // Asignar un idUsuario válido
                        timestamp = DateTime.Now
                    };
                    _context.OdT1.Add(odT1);
                    _context.SaveChanges();

                    mesajeBitacora = $"La orden de trabajo con poNumber = {odT1.poNumber}, " +
                        $"se agrego correctamente.";

                    bitacoraDeCarga1.RegistrarCarga(odT1.idOdT, mesajeBitacora, 1);
                    bitacoraDeCarga1.RegistrarCarga(odT1.idOdT, mesajeBitProducto, 1);

                    for (int j = 2; j < 22; j++)
                    {
                        if (resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"A{j}") == null ||
                            resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"B{j}") == null)
                        {
                            continue; // Si no hay datos en las celdas A o B, saltar a la siguiente iteración
                        }
                        string serialNumber = resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"A{j}").valor;
                        int quantity = int.Parse(resultadoLecturaExcel.dataExcel.FirstOrDefault(d => d.celda == $"B{j}").valor);
                        if (serialNumber != null && quantity != null)
                        {
                            MovimientosOdT1 movimientoOdT1 = new MovimientosOdT1();
                            MovimientosOdT1 movimientoOdT = _context.MovimientosOdT1.FirstOrDefault(m => m.serialNumber == serialNumber && m.idOdT == odT1.idOdT);
                            if (movimientoOdT == null)
                            {
                                movimientoOdT1 = new MovimientosOdT1
                                {
                                    idOdT = odT1.idOdT,
                                    idProducto = producto1.idProducto,
                                    serialNumber = serialNumber,
                                    quantity = quantity,
                                    idUsuario = 1, // Asignar un idUsuario válido
                                    timestamp = DateTime.Now
                                };
                                _context.MovimientosOdT1.Add(movimientoOdT1);
                                _context.SaveChanges();
                                mesajeBitacora = $"El peso de la celda B{j} se agrego correctamente.";
                                bitacoraDeCarga1.RegistrarCarga(odT1.idOdT, mesajeBitacora, 1);
                            }
                            else
                            {
                                mesajeBitacora = $"Ya existe un peso registrado para el serialNumber = {serialNumber}, " +
                                    $"no pueden existir 2 serialNumber en la misma orden de trabajo.";
                                bitacoraDeCarga1.RegistrarCarga(odT1.idOdT, mesajeBitacora, 1);
                                resultadoLecturaExcel.mensaje.Add
                                (
                                    mesajeBitacora
                                );
                            }
                        }
                    }
                    var movimientos = (from m in _context.MovimientosOdT1
                                       where m.idProducto == producto1.idProducto && m.idOdT == odT1.idOdT
                                       select new { m.quantity }).ToList();
                    int surtido = movimientos.Sum(m => m.quantity);
                    resultadoLecturaExcel.poNumber = odT1.poNumber;
                    resultadoLecturaExcel.partNumber = producto1.partNumber;
                    resultadoLecturaExcel.surtido = surtido;
                    resultadoLecturaExcel.totalKgOT = odT1.totalKgOT;
                    resultadoLecturaExcel.porSurtir = odT1.totalKgOT - surtido;
                }
                else
                {
                    resultadoLecturaExcel.error = true;
                    resultadoLecturaExcel.mensaje.Add
                    (
                        $"Ya existe una orden de trabajo con poNumber = {odT.poNumber}, " +
                        $"no pueden existir 2 ordenes de trabajo con el mismo poNumber y mismo producto."
                    );
                }
            }
            //else
            //{
            //    resultadoLecturaExcel.error = true;
            //    resultadoLecturaExcel.mensaje.Add
            //    (
            //        $"No se puede agregar una orden de trabajo sin producto, orden de trabajo " +
            //        $"o peso requerido, revise celdas G2, G3 y G4."
            //    );
            //}

            return resultadoLecturaExcel;
        }
    }
}

