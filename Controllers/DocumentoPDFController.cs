using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Spreadsheet;
using Spire.Doc;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Cors;
using Polyempaques_API.Models;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Paragraph = Spire.Doc.Documents.Paragraph;
using System.Drawing;
using QRCoder;
using System.Drawing.Imaging;

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
        //SOLO ES UN COMENTARIO
        private void AgregarTextoAlMarcador(
            List<DocumentFormat.OpenXml.Wordprocessing.BookmarkStart> bookmarks,
            string NombreMarcador,
            string valor,
            bool bold = false,
            bool underline = false,
            string fontFamily = "",
            string fontSize = "")
        {
            var bm = bookmarks.FirstOrDefault(bms => bms.Name == NombreMarcador);
            Run r = bm.Parent.InsertAfter(new Run(), bm);
            DocumentFormat.OpenXml.Wordprocessing.RunProperties rp = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
            if (bold)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Bold());
            }
            if (underline)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single });
            }
            if (fontFamily != "")
            {
                rp.AppendChild(new RunFonts { Ascii = fontFamily });
            }
            if (fontSize != "")
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.FontSize { Val = new StringValue(fontSize) });
            }
            if (underline)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single });
            }
            r.AppendChild(rp);
            r.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Text(valor));
        }

        private void AgregarListaTextosAlMarcador(
            List<DocumentFormat.OpenXml.Wordprocessing.BookmarkStart> bookmarks,
            string NombreMarcador,
            List<string> lista,
            bool bold = false,
            bool underline = false,
            string fontFamily = "",
            string fontSize = "")
        {
            var bm = bookmarks.FirstOrDefault(bms => bms.Name == NombreMarcador);
            Run r = bm.Parent.InsertAfter(new Run(), bm);
            DocumentFormat.OpenXml.Wordprocessing.RunProperties rp = new DocumentFormat.OpenXml.Wordprocessing.RunProperties();
            if (bold)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Bold());
            }
            if (underline)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single });
            }
            if (fontFamily != "")
            {
                rp.AppendChild(new RunFonts { Ascii = fontFamily });
            }
            if (fontSize != "")
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.FontSize { Val = new StringValue(fontSize) });
            }
            if (underline)
            {
                rp.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single });
            }
            r.AppendChild(rp);
            for (int i = 0; i < lista.Count; i++)
            {
                r.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Text(lista[i]));
                r.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Break());

            }
        }

        private void AgregarImagenAlMarcador(
            List<DocumentFormat.OpenXml.Wordprocessing.BookmarkStart> bookmarks,
            string NombreMarcador,
            string relationshipId)
        {

            var bm = bookmarks.FirstOrDefault(bms => bms.Name == NombreMarcador);
            Run r = bm.Parent.InsertAfter(new Run(GetImageElement(relationshipId)), bm);
        }

        private static DocumentFormat.OpenXml.Wordprocessing.Drawing GetImageElement(string relationshipId)
        {
            var element = new DocumentFormat.OpenXml.Wordprocessing.Drawing(
                new DW.Inline(
                     new DW.Extent() { Cx = 990000L, Cy = 990000L },
                     new DW.EffectExtent()
                     {
                         LeftEdge = 0L,
                         TopEdge = 0L,
                         RightEdge = 0L,
                         BottomEdge = 0L
                     },
                     new DW.DocProperties()
                     {
                         Id = (UInt32Value)1U,
                         Name = "Picture 1"
                     },
                     new DW.NonVisualGraphicFrameDrawingProperties(
                         new A.GraphicFrameLocks() { NoChangeAspect = true }),
                     new A.Graphic(
                         new A.GraphicData(
                             new PIC.Picture(
                                 new PIC.NonVisualPictureProperties(
                                     new PIC.NonVisualDrawingProperties()
                                     {
                                         Id = (UInt32Value)0U,
                                         Name = "New Bitmap Image.jpg"
                                     },
                                     new PIC.NonVisualPictureDrawingProperties()),
                                 new PIC.BlipFill(
                                     new A.Blip(
                                         new A.BlipExtensionList(
                                             new A.BlipExtension()
                                             {
                                                 Uri =
                                                    "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                             })
                                     )
                                     {
                                         Embed = relationshipId,
                                         CompressionState =
                                         A.BlipCompressionValues.Print
                                     },
                                     new A.Stretch(
                                         new A.FillRectangle())),
                                 new PIC.ShapeProperties(
                                     new A.Transform2D(
                                         new A.Offset() { X = 0L, Y = 0L },
                                         new A.Extents() { Cx = 990000L, Cy = 990000L }),
                                     new A.PresetGeometry(
                                         new A.AdjustValueList()
                                     )
                                     { Preset = A.ShapeTypeValues.Rectangle }))
                         )
                         { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                 )
                {
                    DistanceFromTop = (UInt32Value)0U,
                    DistanceFromBottom = (UInt32Value)0U,
                    DistanceFromLeft = (UInt32Value)0U,
                    DistanceFromRight = (UInt32Value)0U,
                    EditId = "50D07946"
                });
            return element;
        }
        [HttpPost]
        [Route("[action]")]
        [EnableCors("AllowAnyOrigin")]
        public IActionResult GeneraEtiqueta([FromBody] QR qr)
        {
            try
            {
                var fechayhora = DateTime.UtcNow.ToString("yyyy-MM-ddThh-mm-ss");
                string pathPlantilla = $"{Environment.CurrentDirectory}\\template\\PlantillaEtiqueta.docx";

                // NOMBRE DEL PDF QUE SE CREARA
                string archivoDeSalida = $"{Environment.CurrentDirectory}\\temp\\PlantillaEtiqueta_{fechayhora}.pdf";

                // ARCHIVO TEMPORAL EN BASE A LA PLANTILLA
                string archivoTemporal = $"{Environment.CurrentDirectory}\\temp\\PlantillaEtiqueta_{fechayhora}.docx";

                // Create shadow File
                System.IO.File.Copy(pathPlantilla, archivoTemporal, true);

                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(archivoTemporal, true))
                {
                    var main = wordDoc.MainDocumentPart.Document;
                    //var bookmarksHeader = wordDoc.MainDocumentPart.HeaderParts.FirstOrDefault().RootElement.Descendants<DocumentFormat.OpenXml.Wordprocessing.BookmarkStart>().ToList();
                    var bookmarks = main.Descendants<DocumentFormat.OpenXml.Wordprocessing.BookmarkStart>().ToList();
                    AgregarTextoAlMarcador(bookmarks, "descripcion", qr.descripcion, true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "partNumber", qr.partNumber, true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "quantity", qr.quantity.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "poNumber", qr.poNumber.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "trace", qr.trace.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "serialNumber", qr.serialNumber.ToString(), true, false, "Arial Narrow", "72");
                    //AgregarTextoAlMarcador(bookmarks, "qrData", $"P{qr.partNumber},Q{qr.quantity},K{qr.poNumber},N{qr.serialNumber}", false, false, "Arial Narrow", "15");

                    AgregarTextoAlMarcador(bookmarks, "descripcion2", qr.descripcion, true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "partNumber2", qr.partNumber, true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "quantity2", qr.quantity.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "poNumber2", qr.poNumber.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "trace2", qr.trace.ToString(), true, false, "Arial Narrow", "72");
                    AgregarTextoAlMarcador(bookmarks, "serialNumber2", qr.serialNumber.ToString(), true, false, "Arial Narrow", "72");
                    //AgregarTextoAlMarcador(bookmarks, "qrData2", $"P{qr.partNumber},Q{qr.quantity},K{qr.poNumber},N{qr.serialNumber}", false, false, "Arial Narrow", "15");
                    main.Save();
                }

                //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode($"P{qr.partNumber},Q{qr.quantity},K{qr.poNumber},N{qr.serialNumber}", QRCodeGenerator.ECCLevel.Q);
                //PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                //byte[] qrCodeImage = qrCode.GetGraphic(7);

                DataMatrix.net.DmtxImageEncoder encoder = new DataMatrix.net.DmtxImageEncoder();
                Bitmap bmp = encoder.EncodeImage($"P{qr.partNumber},Q{qr.quantity},K{qr.poNumber},N{qr.serialNumber}");
                Bitmap resized = new Bitmap(bmp, new Size(275, 275));
                //bmp.Save("Helloworld.png", System.Drawing.Imaging.ImageFormat.Png);

                MemoryStream ms = new MemoryStream();
                resized.Save(ms, ImageFormat.Bmp);
                byte[] qrCodeImage = ms.ToArray();

                Spire.Doc.Document document = new Spire.Doc.Document();
                document.LoadFromFile(archivoTemporal);

                //Create an instance of BookmarksNavigator
                BookmarksNavigator bn = new BookmarksNavigator(document);
                //Find a bookmark and its name is Spire
                bn.MoveToBookmark("qr", true, true);

                //Add a section and named it section0
                Section section0 = document.AddSection();
                //Add a paragraph for section0
                Paragraph paragraph = section0.AddParagraph();
                //Image image = Image.FromFile(qrCodeImage);
                //Add a picture into paragraph
                //DocPicture picture = paragraph.AppendPicture(qrCodeImage);
                DocPicture picture = paragraph.AppendPicture(qrCodeImage);
                //Add a paragraph with picture at the position of bookmark
                bn.InsertParagraph(paragraph);
                document.Sections.Remove(section0);

                //Find a bookmark and its name is Spire
                bn.MoveToBookmark("qr2", true, true);

                //Add a section and named it section1
                Section section1 = document.AddSection();
                //Add a paragraph for section1
                Paragraph paragraph1 = section1.AddParagraph();
                //Add a picture into paragraph1
                //DocPicture picture1 = paragraph1.AppendPicture(qrCodeImage);
                DocPicture picture1 = paragraph1.AppendPicture(qrCodeImage);
                //Add a paragraph with picture at the position of bookmark
                bn.InsertParagraph(paragraph1);
                document.Sections.Remove(section1);

                document.SaveToFile(archivoDeSalida, FileFormat.PDF);
                System.IO.File.Delete(archivoTemporal);
                byte[] FileByteData = System.IO.File.ReadAllBytes(archivoDeSalida);
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
