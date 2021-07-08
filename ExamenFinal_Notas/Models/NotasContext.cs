using ExamenFinal_Notas.Models.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models
{
    public class NotasContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<Etiqueta_Nota> Etiqueta_Notas { get; set; }
        public DbSet<Nota_Compartida> Nota_Compartidas { get; set; }
        public NotasContext(DbContextOptions<NotasContext> options):
            base(options){ }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new NotaMap());
            modelBuilder.ApplyConfiguration(new Etiqueta_NotaMap());
            modelBuilder.ApplyConfiguration(new EtiquetaMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new Nota_CompartidaMap());
        }

        
    }
}
