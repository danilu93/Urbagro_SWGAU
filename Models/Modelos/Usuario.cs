using SWGAU.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGAU.Models.Modelos
{
    public class Usuario 
    { 
        public int UsuarioId { get; set; }
        
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede tener más de 50 caracteres.")]
        public string NombreUsuario { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Display(Name = "Contraseña")]
        [Column(TypeName = "nvarchar(100)")]
        public string ContrasenaHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [Display(Name = "Correo Electrónico")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [Display(Name = "Rol")]
        public RolUsuario Rol { get; set; } 

        public bool Activo { get; set; } = true;


    }
}
