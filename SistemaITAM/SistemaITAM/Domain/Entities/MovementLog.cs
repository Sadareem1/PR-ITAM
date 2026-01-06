using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Domain.Entities;

public class MovementLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ActivoId { get; set; }
    public Guid? AdministradorId { get; set; }
    public TipoMovimiento TipoMovimiento { get; set; }
    public string DescripcionDetallada { get; set; } = string.Empty;
    public DateTime FechaMovimiento { get; set; } = DateTime.UtcNow;
    public string PlantaOrigen { get; set; } = string.Empty;
    public string AreaOrigen { get; set; } = string.Empty;
    public ModuloSistema Modulo { get; set; } = ModuloSistema.Activos;
}
