using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model
{
    [Table("Ventas")]
    public class Venta
    {
        [Key]
        public int VentaID { get; set; }

        [Required]
        [MaxLength(50)]
        public string NumeroVenta { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Impuesto { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [MaxLength(50)]
        public string MetodoPago { get; set; }

        [MaxLength(50)]
        public string? EstadoVenta { get; set; } = "Completada";

        public int? UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario? Usuario { get; set; }

        [MaxLength(500)]
        public string? Observaciones { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual ICollection<DetalleVenta>? DetallesVenta { get; set; }
    }
}