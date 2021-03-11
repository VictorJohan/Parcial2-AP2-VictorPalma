using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2_AP2_VictorPalma.Models
{
    public class Cobros
    {
        [Key]
        public int CobroId { get; set; }
        public string Observaciones { get; set; }
        public DateTime Fecha { get; set; }
        public int Conteo { get; set; }
        public double TotalCobrado { get; set; }
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Clientes Cliente { get; set; }

        [ForeignKey("CobroId")]
        public virtual List<CobrosDetalles> CobrosDetalle { get; set; }

        public Cobros()
        {
            Fecha = DateTime.Now;
            CobrosDetalle = new List<CobrosDetalles>();
        }
    }
}
