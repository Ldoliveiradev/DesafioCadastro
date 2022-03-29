using Cadastro.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Cadastro.Infra.Context
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
