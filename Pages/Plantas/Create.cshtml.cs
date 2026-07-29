using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;

// Page para crear una planta nueva

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;  // Contexto de base de datos

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Planta Planta { get; set; } = new();  // Nueva planta a registrar

        public void OnGet() { }

        // Guarda la nueva planta en la base de datos
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Planta.FechaRegistro = DateTime.Now;  // Asigna la fecha de registro automáticamente

            _context.Plantas.Add(Planta);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = $"La planta \"{Planta.NombrePlanta}\" se registró correctamente.";
            return RedirectToPage("Index");
        }
    }
}
