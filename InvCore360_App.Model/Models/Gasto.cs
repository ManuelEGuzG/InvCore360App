using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("Gastos")]
    public class Gasto
    {
        [Key]
        public int GastoID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Concepto { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [MaxLength(100)]
        public string? Categoria { get; set; }

        public DateTime FechaGasto { get; set; }

        [MaxLength(50)]
        public string? MetodoPago { get; set; }

        public int? UsuarioID { get; set; }
        [ForeignKey("UsuarioID")]
        public virtual Usuario? Usuario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}