namespace SistemaITAM.Application.DTOs;

public record DashboardSummaryDto(
    int TotalActivos,
    int ActivosAsignados,
    int UsuariosActivos,
    int PlantasActivas,
    int AreasActivas,
    IEnumerable<PlantaResumenDto> ResumenPorPlanta,
    IEnumerable<MovimientoRecienteDto> UltimosMovimientos);

public record PlantaResumenDto(string Planta, int TotalActivos, int TotalUsuarios, int AsignacionesActivas);
public record MovimientoRecienteDto(string Descripcion, DateTime Fecha, string Planta, string Area, string Tipo);
