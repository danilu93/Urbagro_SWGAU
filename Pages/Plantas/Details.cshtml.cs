using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SWGAU.Models;
using SWGAU.Models.Modelos;

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public Planta Planta { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var planta = await _context.Plantas.FindAsync(id);
            if (planta == null) return RedirectToPage("Index");

            Planta = planta;
            return Page();
        }
    }
}