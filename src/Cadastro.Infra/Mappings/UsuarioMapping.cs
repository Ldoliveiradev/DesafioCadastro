using Cadastro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cadastro.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.OwnsOne(u => u.Email, a =>
            {
                a.Property(e => e.Endereco)
                .HasColumnName("Email")
                .HasMaxLength(30)
                .IsRequired();
            });

            builder.Property(u => u.Telefone)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}
