using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models.Maps
{
    public class Etiqueta_NotaMap : IEntityTypeConfiguration<Etiqueta_Nota>
    {
        public void Configure(EntityTypeBuilder<Etiqueta_Nota> builder)
        {
            builder.ToTable("Etiqueta_Nota");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Etiqueta).WithMany().
                HasForeignKey(o => o.EtiquetaId);

        }
    }
}
