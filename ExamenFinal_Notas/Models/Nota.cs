using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public DateTime DateRegister { get; set; }
        public string Nota_Content { get; set; }
        public int UserId { get; set; }
    }
}
