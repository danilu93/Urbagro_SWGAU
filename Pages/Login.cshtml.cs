using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

// Page para iniciar sesión

namespace SWGAU.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;  // Contexto de base de datos

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string NombreUsuario { get; set; } = string.Empty;  // Nombre de usuario del formulario

        [BindProperty]
        public string Contrasena { get; set; } = string.Empty;     // Contraseña del formulario
        public string? MensajeError { get; set; }                   // Mensaje de error para mostrar al usuario

        public void OnGet()
        {
        }

        // Valida credenciales e inicia sesión si son correctas
        public async Task<IActionResult> OnPostAsync()
        {
            // Verifica que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(NombreUsuario) || string.IsNullOrWhiteSpace(Contrasena))
            {
                MensajeError = "Por favor ingrese usuario y contraseña.";
                return Page();
            }

            // Busca el usuario activo en la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario && u.Activo);

            // Verifica la contraseña con BCrypt
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(Contrasena, usuario.ContrasenaHash))
            {
                MensajeError = "Usuario o contraseña incorrectos.";
                return Page();
            }

            // Crea los claims de autenticación
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            // Inicia la sesión con cookies de autenticación
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Plantas/Index");
        }
    }
}
