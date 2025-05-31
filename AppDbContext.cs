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
    }
}
