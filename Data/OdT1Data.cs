using Polyempaques_API.Models;

namespace Polyempaques_API.Data
{
    public class OdT1Data
    {
        private readonly AppDbContext _context;

        public OdT1Data(AppDbContext context)
        {
            this._context = context;
        }

        public class GetOdTs
        {
            public int idOdT { get; set; }
            public string poNumber { get; set; }
            public int totalKgOT { get; set; }
            public DateTime? timestamp { get; set; }
            public string descripcion { get; set; }
            public string partNumber { get; set; }
            public int surtido { get; set; }
        }
        public List<GetOdTs> ConsultaOdTs()
        {
            List<GetOdTs> getOdTs = new List<GetOdTs>();
            var odts = (from o in _context.OdT1
                        join p in _context.Producto1 on o.idProducto equals p.idProducto
                        where o.activo == true
                        select new
                        {
                            o.idOdT,
                            o.poNumber,
                            o.totalKgOT,
                            o.timestamp,
                            p.descripcion,
                            p.partNumber
                        }).ToList();
            foreach (var odt in odts)
            {
                int surtido = (from m in _context.MovimientosOdT1
                              where m.idOdT == odt.idOdT
                              select m.quantity).Sum();
                getOdTs.Add(new GetOdTs
                {
                    idOdT = odt.idOdT,
                    poNumber = odt.poNumber,
                    totalKgOT = odt.totalKgOT,
                    timestamp = odt.timestamp,
                    descripcion = odt.descripcion,
                    partNumber = odt.partNumber,
                    surtido = surtido
                });
            }
            return getOdTs;
        }
        public void eliminarOdT(int idOdT)
        {
            OdT1 odt = _context.OdT1.FirstOrDefault(o => o.idOdT == idOdT);
            odt.activo = false;
            _context.OdT1.Update(odt);
            _context.SaveChanges();
        }
    }
}
