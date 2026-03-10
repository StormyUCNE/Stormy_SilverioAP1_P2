using System.ComponentModel.DataAnnotations;

namespace Stormy_SilverioAP1_P2.Models;

public class DetalleAsignacion
{
    [Key]
    public int IdDetalle { get; set; }
    public int IdAsignacion { get; set; }
    public int TipoPuntoId { get; set; }
    public int CantidadPuntos { get; set; }
}
