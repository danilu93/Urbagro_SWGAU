using SWGAU.Models.Enums;
using SWGAU.Pages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGAU.Models.Modelos
{
    // Clase que representa un registro de irrigación en la base de datos.
    public class Irrigacion
    {
        public int IrrigacionId { get; set; } // Identificador único de la irrigación

        [Required]
        [Display(Name = "Planta")]
        public int PlantaId { get; set; }                               

        [Required(ErrorMessage = "La fecha de riego es obligatoria.")]
        [Display(Name = "Fecha de Riego")]
        public DateTime FechaRiego { get; set; } = DateTime.Now;        
        [Required(ErrorMessage = "El método de riego es obligatorio.")]
        [Display(Name = "Método de Riego")]
        public MetodoRiego MetodoRiego { get; set; }                    

        [Required(ErrorMessage = "La frecuencia de riego es obligatoria.")]
        [Display(Name = "Frecuencia de Riego")]
        public FrecuenciaRiego FrecuenciaRiego { get; set; }           

        [Required(ErrorMessage = "La duración del riego es obligatoria.")]
        [Column(TypeName = "decimal(6,2)")]
        [Display(Name = "Duración del Riego (min)")]
        public decimal? DuracionRiego { get; set; }                    

        [Required(ErrorMessage = "La cantidad de agua es obligatoria.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cantidad de Agua (ml)")]
        public decimal? CantidadAgua { get; set; }                      

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }                      

        [ForeignKey("PlantaId")]
        public Planta? Planta { get; set; } // Propiedad de navegación a la planta
    }
}
