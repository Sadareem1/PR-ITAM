using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Domain.Entities;

public class Activo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CodigoPatrimonial { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Marca { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public TipoActivo TipoActivo { get; set; }
    public EstadoActivo EstadoActivo { get; set; } = EstadoActivo.Disponible;
    public Guid PlantaId { get; set; }
    public Guid AreaId { get; set; }
    public string Observaciones { get; set; } = string.Empty;
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
}
