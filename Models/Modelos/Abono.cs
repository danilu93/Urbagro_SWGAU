using SWGAU.Models.Enums;
using SWGAU.Pages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWGAU.Models.Modelos
{
    // Clase que representa un registro de abono en la base de datos.
    public class Abono
    {
        public int AbonoId { get; set; } // Identificador único del abono

        [Required]
        [Display(Name = "Planta")]
        public int PlantaId { get; set; }                               

        [Required(ErrorMessage = "La fecha de aplicación es obligatoria.")]
        [Display(Name = "Fecha de Abono")]
        public DateTime FechaAbono { get; set; } = DateTime.Now;        

        [Required(ErrorMessage = "La frecuencia de aplicación es obligatoria.")]
        [Display(Name = "Frecuencia de Abono")]
        public FrecuenciaAbono FrecuenciaAbono { get; set; }            

        [Required(ErrorMessage = "El tipo de abono es obligatorio.")]
        [Display(Name = "Tipo de Abono")]
        public TipoAbono TipoAbono { get; set; }                        

        [Display(Name = "Nombre del Abono")]
        [StringLength(100)]
        public string? NombreAbono { get; set; }                        

        [Required(ErrorMessage = "La cantidad de abono es obligatoria.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cantidad de Abono (ml o gr)")]
        public decimal? CantidadAbono { get; set; }                    

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; }                     

        [ForeignKey("PlantaId")]
        public Planta? Planta { get; set; } // Propiedad de navegación a la planta
    }
}
