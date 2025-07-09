using Microsoft.EntityFrameworkCore;
using Polyempaques_API.Models;

namespace Polyempaques_API
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Modulos> Modulos { get; set; }
        public DbSet<Operaciones> Operaciones { get; set; }
        public DbSet<Perfiles> Perfiles { get; set; }
        public DbSet<PerfilOperacion> PerfilOperacion { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<QR> QR { get; set; }
        public DbSet<Producto1> Producto1 { get; set; }
        public DbSet<OdT1> OdT1 { get; set; }
        public DbSet<MovimientosOdT1> MovimientosOdT1 { get; set; }
        public DbSet<BitacoraDeCarga1> BitacoraDeCarga1 { get; set; }
    }
}
