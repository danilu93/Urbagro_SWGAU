using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SWGAU.Models;
using SWGAU.Models.Enums;
using SWGAU.Models.Modelos;

namespace SWGAU.Pages
{
    public class RegistroUsuarioModel : PageModel
    {

        private readonly AppDbContext _context;

        public RegistroUsuarioModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegistroInput Input { get; set; } = new();

        public string? MensajeError { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool exiteUsuario = await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == Input.NombreUsuario);
            if (exiteUsuario)
            {
                MensajeError = "Ese nomnre de usuario ya existe.";
                return Page();
            }

            bool existeCorreo = await _context.Usuarios
                .AnyAsync(u => u.CorreoElectronico == Input.CorreoElectronico);

            if (existeCorreo)
            {
                MensajeError = "Ese correo electrónico ya está registrado.";
                return Page();
            }

            var usuario = new Usuario
            {
                NombreUsuario = Input.NombreUsuario,
                CorreoElectronico = Input.CorreoElectronico,
                Rol = Input.Rol,
                Activo = true
            };

            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(Input.Contrasena);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");

        }

        public class RegistroInput
        {
            [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
            [StringLength(50, ErrorMessage = "Debe tener máximo 50 caracteres.")]
            [Display(Name = "Nombre de Usuario")]
            public string NombreUsuario { get; set; } = string.Empty;


            [Required(ErrorMessage = "El correo electronico es obligatorio.")]
            [StringLength(50, ErrorMessage = "El correo electronico no es valido.")]
            [Display(Name = "Correo Electronico")]
            public string CorreoElectronico { get; set; } = string.Empty;


            [Required(ErrorMessage = "La contraseña es obligatoria.")]
            [DataType(DataType.Password)]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Debe tener entre 6 y 100 caracteres.")]
            [Display(Name = "Contraseña")]
            public string Contrasena { get; set; } = string.Empty;


            [Required(ErrorMessage = "Confirme la contraseña.")]
            [DataType(DataType.Password)]
            [Compare(nameof(Contrasena),ErrorMessage = "Las contraseñas no coinciden.")]
            [Display(Name = "Confirmar Contraseña")]
            public string ConfirmarContrasena { get; set; } = string.Empty;


            [Required(ErrorMessage = "El rol es obligatorio.")]
            [Display(Name = "Rol")]
            public RolUsuario Rol { get; set; }
        }
    }
}