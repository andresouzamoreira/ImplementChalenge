using ImplementChallenge.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImplementChallenge.Api.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasColumnType("varchar(255)");
            builder.Property(p => p.Senha).HasColumnType("varchar(255)");
            builder.Property(p => p.Token).HasColumnType("varchar(255)");
            builder.Property(p => p.TipoClaim).HasColumnType("varchar(50)");
            builder.Property(p => p.ValorClaim).HasColumnType("varchar(50)");
        }
    }
}
