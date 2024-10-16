using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Divisas.Models
{
    public class Sucursal
    {
        [Key]
        public int id { get; set; }
        [MaxLength(50)]
        public string? nombre_empresa { get; set; }
        [MaxLength(50)]
        [Required]
        public string? direccion { get; set; }
        [MaxLength(50)]
        public string? direccion2 { get; set; }
        [MaxLength(5)]
        public int codigo_postal { get; set; }
        [MaxLength(50)]
        [Required]
        public string? ciudad { get; set; }
        [MaxLength(50)]
        [Required]
        public string? estado { get; set; }
        [Required]
        public string? foto { get; set; }

    }
}
