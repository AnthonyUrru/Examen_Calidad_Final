using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models
{
    public class Etiqueta_Nota
    {
        public int Id { get; set; }
        public int EtiquetaId { get; set; }
        public int NotaId { get; set; }
        public Etiqueta Etiqueta { get; set; }
    }
}
