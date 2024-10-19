using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Divisas.Models
{
    public class Moneda
    {
        [Key]
        public int id { get; set; }
        [MaxLength(3)]
        public string? clave { get; set; }

        [MaxLength(10)]
        public decimal valor_compra { get; set; }

        [MaxLength(10)]
        public decimal valor_venta { get; set; }
    }
}
