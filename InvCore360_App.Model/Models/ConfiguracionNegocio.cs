using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("ConfiguracionNegocio")]
    public class ConfiguracionNegocio
    {
        [Key]
        public int ConfigID { get; set; }

        [Required]
        [MaxLength(200)]
        public string NombreNegocio { get; set; }

        [MaxLength(50)]
        public string? RUC { get; set; }

        [MaxLength(300)]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal PorcentajeImpuesto { get; set; } = 0;

        [MaxLength(10)]
        public string? MonedaPredeterminada { get; set; } = "CRC";

        public bool AlertasHabilitadas { get; set; } = true;

        public int DiasParaReportes { get; set; } = 30;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}