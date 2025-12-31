using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("HistorialPrecios")]
    public class HistorialPrecio
    {
        [Key]
        public int HistorialID { get; set; }

        public int ProductoID { get; set; }
        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrecioCostoAnterior { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrecioCostoNuevo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrecioVentaAnterior { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrecioVentaNuevo { get; set; }

        public DateTime FechaCambio { get; set; } = DateTime.Now;

        public int? UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario? Usuario { get; set; }
    }
}