using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace SWGAU.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string NombreUsuario { get; set; } = string.Empty;

        [BindProperty]
        public string Contrasena { get; set; } = string.Empty;
        public string? MensajeError { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NombreUsuario) || string.IsNullOrWhiteSpace(Contrasena))
            {
                MensajeError = "Por favor ingrese usuario y contraseña.";
                return Page();
            }


            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == NombreUsuario && u.Activo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(Contrasena, usuario.ContrasenaHash))
            {
                MensajeError = "Usuario o contraseña incorrectos.";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Plantas/Index");
        }
    }
}
