using SWGAU.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGAU.Models.Modelos
{
    public class Usuario 
    { 
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string NombreUsuario { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Column(TypeName = "nvarchar(100)")]
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public RolUsuario Rol { get; set; } 

        public bool Activo { get; set; } = true;


    }
}
