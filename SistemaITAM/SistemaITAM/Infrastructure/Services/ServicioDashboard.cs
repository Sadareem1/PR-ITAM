using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class ServicioDashboard : IServicioDashboard
{
    private readonly ContextoDatosEnMemoria _context;
    private readonly IServicioMovimientos _movementLogService;

    public ServicioDashboard(ContextoDatosEnMemoria context, IServicioMovimientos movementLogService)
    {
        _context = context;
        _movementLogService = movementLogService;
    }

    public async Task<ResumenDashboardDto> BuildAsync()
    {
        var resumenPlanta = _context.Plantas.Select(planta =>
        {
            var activos = _context.Activos.Count(a => a.PlantaId == planta.Id);
            var usuarios = _context.Usuarios.Count(u => u.PlantaId == planta.Id);
            var asignaciones = _context.Asignaciones.Count(a => _context.Activos.First(act => act.Id == a.ActivoId).PlantaId == planta.Id);
            return new PlantaResumenDto(planta.Nombre, activos, usuarios, asignaciones);
        }).ToList();

        var movimientos = await _movementLogService.GetAsync(new Application.Filters.FiltroMovimientos());
        var ultimos = movimientos.Take(6).Select(m => new MovimientoRecienteDto(m.Descripcion, m.Fecha, m.Planta, m.Area, m.Tipo.ToString())).ToList();

        return new ResumenDashboardDto(
            _context.Activos.Count,
            _context.Activos.Count(a => a.EstadoActivo == EstadoActivo.Asignado),
            _context.Usuarios.Count(u => u.Estado == EstadoUsuario.Activo),
            _context.Plantas.Count(p => p.Activa),
            _context.Areas.Count(a => a.Activa),
            resumenPlanta,
            ultimos);
    }
}
