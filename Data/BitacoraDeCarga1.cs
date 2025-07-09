using Polyempaques_API.Models;

namespace Polyempaques_API.Data
{
    public class BitacoraDeCarga1
    {
        private readonly AppDbContext _context;

        public BitacoraDeCarga1(AppDbContext context)
        {
            this._context = context;
        }

        public void RegistrarCarga(int idOdT1, string mensaje, int idUsuario)
        {
            var bitacora = new Models.BitacoraDeCarga1
            {
                idOdT = idOdT1,
                mensaje = mensaje,
                idUsuario = idUsuario,
                timestamp = DateTime.Now // Usar UTC para evitar problemas de zona horaria
            };
            _context.BitacoraDeCarga1.Add(bitacora);
            _context.SaveChanges();
        }
        public List<Models.BitacoraDeCarga1> registrosDeCarga(int idOdT1)
        {
            var registros = (from b in _context.BitacoraDeCarga1
                             where b.idOdT == idOdT1
                             select b).ToList();
            return registros;
        }
    }
}
