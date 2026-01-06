using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Domain.Entities;

public class Asignacion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ActivoId { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid AdministradorId { get; set; }
    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;
    public string RutaPdfActa { get; set; } = string.Empty;
    public EstadoAsignacion EstadoAsignacion { get; set; } = EstadoAsignacion.Activa;
}
