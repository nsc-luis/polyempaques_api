using Polyempaques_API.Models;

namespace Polyempaques_API.Data
{
    public class MovimientosOdT1Data
    {
        private readonly AppDbContext _context;

        public MovimientosOdT1Data(AppDbContext context)
        {
            this._context = context;
        }
        public List<MovimientosOdT1> movtosPorOdT(int idOdT)
        {
            //List<MovimientosOdT1> movimientos = new List<MovimientosOdT1>();
            List<MovimientosOdT1> movimientos = (from m in _context.MovimientosOdT1
                        where m.idOdT == idOdT
                        select m).ToList();
            return movimientos;
        }
    }
}
