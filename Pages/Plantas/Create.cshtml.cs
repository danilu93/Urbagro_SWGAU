using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Planta Planta { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Planta.FechaRegistro = DateTime.Now;

            _context.Plantas.Add(Planta);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = $"La planta \"{Planta.NombrePlanta}\" se registró correctamente.";
            return RedirectToPage("Index");
        }
    }
}