using System.ComponentModel.DataAnnotations;
namespace Stormy_SilverioAP1_P2.Models;

public class Estudiantes
{
    [Key]
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "Este Campo Es Requerido")]

    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]

    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Este Campo Es Requerido")]
    public int Edad { get; set; }
    public int BalancePuntos { get; set; } = 0;
}

