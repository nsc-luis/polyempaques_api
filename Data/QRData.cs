using Polyempaques_API.Models;

namespace Polyempaques_API.Data
{
    public class QRData
    {
        private readonly AppDbContext _context;

        private readonly DateTime fechaActual = DateTime.Now;

        public QRData(AppDbContext context)
        {
            _context = context;
        }

        public void Alta(QR qR)
        {
            qR.timestamp = fechaActual;
            qR.idUsuario = 1;
            qR.activo = true;
            qR.ediciones = 0;
            _context.QR.Add(qR);
            _context.SaveChanges();
        }

        public void Edicion(QR qR)
        {
            var qrSeek = _context.QR.FirstOrDefault(qr => qr.idQR == qR.idQR);
            int? ediciones = qrSeek.ediciones == null || qrSeek.ediciones == 0 ? 1 : qrSeek.ediciones + 1;

            qrSeek.descripcion = qR.descripcion;
            qrSeek.partNumber = qR.partNumber;
            qrSeek.quantity = qR.quantity;
            qrSeek.poNumber = qR.poNumber;
            qrSeek.serialNumber = qR.serialNumber;
            qrSeek.timestamp = fechaActual;
            qrSeek.ediciones = ediciones;
            _context.Update(qrSeek);
            _context.SaveChanges();
        }

        public void Eliminacion(int idQR)
        {
            var qr = _context.QR.FirstOrDefault(qr => qr.idQR == idQR);
            qr.activo = false;
            qr.ediciones = qr.ediciones + 1;
            _context.QR.Update(qr);
            _context.SaveChanges();
        }
    }
}
