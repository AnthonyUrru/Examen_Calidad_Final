using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models.Maps
{
    public class Nota_CompartidaMap : IEntityTypeConfiguration<Nota_Compartida>
    {
        public void Configure(EntityTypeBuilder<Nota_Compartida> builder)
        {
            builder.ToTable("Nota_Compartida");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.nota).WithMany().
                HasForeignKey(o => o.NotaId);

            builder.HasOne(o => o.user).WithMany().
                HasForeignKey(o => o.UserIdC);
        }
    }
}
