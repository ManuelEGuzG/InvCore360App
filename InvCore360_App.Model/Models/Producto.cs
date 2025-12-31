using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("Productos")]
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }

        public int? CategoriaID { get; set; }
        [ForeignKey("CategoriaID")]
        public virtual Categoria? Categoria { get; set; }

        public int? ProveedorID { get; set; }
        [ForeignKey("ProveedorID")]
        public virtual Proveedor? Proveedor { get; set; }

        [MaxLength(50)]
        public string? CodigoBarras { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioCosto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioVenta { get; set; }

        public int StockActual { get; set; } = 0;
        public int StockMinimo { get; set; } = 5;
        public int? StockMaximo { get; set; }

        [MaxLength(50)]
        public string? UnidadMedida { get; set; } = "Unidad";

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public virtual ICollection<HistorialPrecio>? HistorialPrecios { get; set; }
        public virtual ICollection<DetalleVenta>? DetallesVenta { get; set; }
        public virtual ICollection<MovimientoInventario>? MovimientosInventario { get; set; }
        public virtual ICollection<AlertaInventario>? AlertasInventario { get; set; }
    }
}