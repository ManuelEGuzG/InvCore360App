using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model
{
    [Table("AlertasInventario")]
    public class AlertaInventario
    {
        [Key]
        public int AlertaID { get; set; }

        public int ProductoID { get; set; }
        [ForeignKey("ProductoID")]
        public virtual Producto? Producto { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoAlerta { get; set; }

        public int StockActual { get; set; }
        public int StockMinimo { get; set; }

        public DateTime FechaAlerta { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string? EstadoAlerta { get; set; } = "Pendiente";

        public DateTime? FechaResolucion { get; set; }
    }
}