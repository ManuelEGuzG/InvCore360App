using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("DetallesVenta")]
    public class DetalleVenta
    {
        [Key]
        public int DetalleVentaID { get; set; }

        public int VentaID { get; set; }
        [ForeignKey("VentaID")]
        public virtual Venta? Venta { get; set; }

        public int ProductoID { get; set; }
        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }

        [Required]
        [MaxLength(200)]
        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCosto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Descuento { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}