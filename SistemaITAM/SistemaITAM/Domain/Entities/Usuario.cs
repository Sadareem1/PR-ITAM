using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public Guid PlantaId { get; set; }
    public Guid AreaId { get; set; }
    public EstadoUsuario Estado { get; set; } = EstadoUsuario.Activo;

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
}
