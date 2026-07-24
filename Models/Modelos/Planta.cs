using Microsoft.Identity.Client;
using SWGAU.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SWGAU.Models.Modelos
{
    public class Planta
    {
        public int PlantaId { get; set; }

        [Required(ErrorMessage = "El tipo de planta es obligatorio.")]
        [Display(Name = "Tipo de Planta")]
        [StringLength(100)]
        public TipoPlanta TipoPlanta { get; set; }

        [Required(ErrorMessage = "El nombre de la planta es obligatorio.")]
        [Display(Name = "Nombre de la Planta")]
        [StringLength(100)]
        public string NombrePlanta { get; set; } = string.Empty;

        [Display(Name = "Nombre Científico")]
        [StringLength(100)]
        public string? NombreCientifico { get; set; } = string.Empty;

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de siembra es obligatoria.")]
        [Display(Name = "Fecha de Siembra")]
        [DataType(DataType.Date)]
        public DateTime FechaSiembra { get; set; }

        [Display(Name = "Método de Siembra")]
        [StringLength(100)]
        public string? MetodoSiembra { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha en que se registró la planta es obligatoria.")]
        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // public List<Irrigacion>? Irrigaciones { get; set; } 
        // public List<Abono>? Abonos { get; set; } 


    }
}
