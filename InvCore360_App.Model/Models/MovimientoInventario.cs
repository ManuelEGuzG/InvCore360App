using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("MovimientosInventario")]
    public class MovimientoInventario
    {
        [Key]
        public int MovimientoID { get; set; }

        public int ProductoID { get; set; }
        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoMovimiento { get; set; }

        public int Cantidad { get; set; }

        public int StockAnterior { get; set; }
        public int StockNuevo { get; set; }

        [MaxLength(200)]
        public string? Motivo { get; set; }

        public int? VentaID { get; set; }
        [ForeignKey("VentaID")]
        public virtual Venta? Venta { get; set; }

        public int? UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario? Usuario { get; set; }

        public DateTime FechaMovimiento { get; set; } = DateTime.Now;
    }
}