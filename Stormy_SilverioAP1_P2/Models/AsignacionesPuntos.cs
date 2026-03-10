using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stormy_SilverioAP1_P2.Models;

public class AsignacionesPuntos
{
    [Key]
    public int IdAsignacion { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Campo Obligatorio")]
    public int EstudianteId { get; set; }
    public int TotalPuntos { get; set; }

    [ForeignKey("EstudianteId")]
    public Estudiantes? Estudiantes { get; set; }
    public ICollection<DetalleAsignacion> ListaDetalle { get; set; } = new List<DetalleAsignacion>();
}

