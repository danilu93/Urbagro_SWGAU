using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
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
            var planta = await _context.Plantas.FindAsync(Planta.PlantaId);
            if (planta != null)
            {
                _context.Plantas.Remove(planta);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = $"La planta \"{planta.NombrePlanta}\" fue eliminada.";
            }

            return RedirectToPage("Index");
        }
    }
}