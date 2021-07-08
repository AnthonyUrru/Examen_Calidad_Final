using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models
{
    public class Nota_Compartida
    {
            public int Id { get; set; }
            public int NotaId { get; set; }
            public int UserIdC { get; set; }
            public int UserIdR { get; set; }
            public Nota nota { get; set; }
            public User user { get; set; }

    }
}
