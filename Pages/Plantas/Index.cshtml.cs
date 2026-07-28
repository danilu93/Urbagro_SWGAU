using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SWGAU.Models.Enums;
using SWGAU.Models.Modelos;
using SWGAU.Models;

namespace SWGAU.Pages.Plantas
{
    
    [Authorize]
    public class IndexModel : PageModel 
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        
        public IList<Planta> Plantas { get; set; } = new List<Planta>();
        public string? Filtro { get; set; }
        public TipoPlanta? FiltroTipo { get; set; }

        public async Task OnGetAsync(string? filtro, TipoPlanta? filtroTipo)
        {
            Filtro = filtro;
            FiltroTipo = filtroTipo;

            var query = _context.Plantas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(p => p.NombrePlanta.Contains(filtro));
            }

            if (filtroTipo.HasValue)
            {
                query = query.Where(p => p.TipoPlanta == filtroTipo.Value);
            }

            Plantas = await query.OrderBy(p => p.NombrePlanta).ToListAsync();
        }
    }
}
