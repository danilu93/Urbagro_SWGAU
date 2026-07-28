using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Planta Planta { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var planta = await _context.Plantas.FindAsync(id);
            if (planta == null) return RedirectToPage("Index");

            Planta = planta;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var plantaExistente = await _context.Plantas.FindAsync(Planta.PlantaId);
            if (plantaExistente == null) return RedirectToPage("Index");

            plantaExistente.NombrePlanta = Planta.NombrePlanta;
            plantaExistente.NombreCientifico = Planta.NombreCientifico;
            plantaExistente.TipoPlanta = Planta.TipoPlanta;
            plantaExistente.FechaSiembra = Planta.FechaSiembra;
            plantaExistente.MetodoSiembra = Planta.MetodoSiembra;
            plantaExistente.Observaciones = Planta.Observaciones;

            await _context.SaveChangesAsync();

            TempData["Mensaje"] = $"La planta \"{Planta.NombrePlanta}\" se actualizó correctamente.";
            return RedirectToPage("Index");
        }
    }
}