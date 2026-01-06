using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly InMemoryDataContext _context;
    private readonly IMovementLogService _movementLogService;

    public DashboardService(InMemoryDataContext context, IMovementLogService movementLogService)
    {
        _context = context;
        _movementLogService = movementLogService;
    }

    public async Task<DashboardSummaryDto> BuildAsync()
    {
        var resumenPlanta = _context.Plantas.Select(planta =>
        {
            var activos = _context.Activos.Count(a => a.PlantaId == planta.Id);
            var usuarios = _context.Usuarios.Count(u => u.PlantaId == planta.Id);
            var asignaciones = _context.Asignaciones.Count(a => _context.Activos.First(act => act.Id == a.ActivoId).PlantaId == planta.Id);
            return new PlantaResumenDto(planta.Nombre, activos, usuarios, asignaciones);
        }).ToList();

        var movimientos = await _movementLogService.GetAsync(new Application.Filters.MovementFilter());
        var ultimos = movimientos.Take(6).Select(m => new MovimientoRecienteDto(m.Descripcion, m.Fecha, m.Planta, m.Area, m.Tipo.ToString())).ToList();

        return new DashboardSummaryDto(
            _context.Activos.Count,
            _context.Activos.Count(a => a.EstadoActivo == EstadoActivo.Asignado),
            _context.Usuarios.Count(u => u.Estado == EstadoUsuario.Activo),
            _context.Plantas.Count(p => p.Activa),
            _context.Areas.Count(a => a.Activa),
            resumenPlanta,
            ultimos);
    }
}
