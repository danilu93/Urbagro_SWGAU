using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SWGAU.Models.Enums;
using SWGAU.Models.Modelos;
using SWGAU.Models;

// Page principal de la gestión de plantas

namespace SWGAU.Pages.Plantas
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;  // Contexto de base de datos

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Planta> Plantas { get; set; } = new List<Planta>(); // Lista de plantas a mostrar
        public string? Filtro { get; set; }                               // Texto de búsqueda por nombre
        public TipoPlanta? FiltroTipo { get; set; }                       // Filtro por tipo de planta

        // Carga la lista de plantas con filtros opcionales
        public async Task OnGetAsync(string? filtro, TipoPlanta? filtroTipo)
        {
            Filtro = filtro;
            FiltroTipo = filtroTipo;

            var query = _context.Plantas.AsQueryable();

            // Filtra por nombre si se proporcionó un texto de búsqueda
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(p => p.NombrePlanta.Contains(filtro));
            }

            // Filtra por tipo de planta si se seleccionó uno
            if (filtroTipo.HasValue)
            {
                query = query.Where(p => p.TipoPlanta == filtroTipo.Value);
            }

            Plantas = await query.OrderBy(p => p.NombrePlanta).ToListAsync();
        }
    }
}
