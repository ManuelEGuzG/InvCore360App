using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvCore360_App.Model.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [MaxLength(100)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Contrasena { get; set; }

        [Required]
        [MaxLength(200)]
        public string NombreCompleto { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Rol { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? UltimoAcceso { get; set; }

        public virtual ICollection<Venta>? Ventas { get; set; }
        public virtual ICollection<MovimientoInventario>? MovimientosInventario { get; set; }
        public virtual ICollection<Gasto>? Gastos { get; set; }
    }
}