using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Movimiento
    {
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Saldo { get; set; }
        public decimal Valor { get; set; }
        public decimal Cupo { get; set; }
    }
}
