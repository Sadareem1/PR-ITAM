using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class ServicioMovimientos : IServicioMovimientos
{
    private readonly ContextoDatosEnMemoria _context;

    public ServicioMovimientos(ContextoDatosEnMemoria context)
    {
        _context = context;
    }

    public Task<IReadOnlyCollection<MovimientoDto>> GetAsync(FiltroMovimientos filter)
    {
        var query = _context.Movimientos.AsEnumerable();

        if (filter.ActivoId.HasValue)
        {
            query = query.Where(m => m.ActivoId == filter.ActivoId.Value);
        }

        if (filter.Tipo.HasValue)
        {
            query = query.Where(m => m.TipoMovimiento == filter.Tipo.Value);
        }

        if (filter.PlantaId.HasValue)
        {
            var planta = _context.Plantas.First(p => p.Id == filter.PlantaId.Value).Nombre;
            query = query.Where(m => string.Equals(m.PlantaOrigen, planta, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.AreaId.HasValue)
        {
            var area = _context.Areas.First(a => a.Id == filter.AreaId.Value).Nombre;
            query = query.Where(m => string.Equals(m.AreaOrigen, area, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.Desde.HasValue)
        {
            query = query.Where(m => m.FechaMovimiento >= filter.Desde.Value);
        }

        if (filter.Hasta.HasValue)
        {
            query = query.Where(m => m.FechaMovimiento <= filter.Hasta.Value);
        }

        var mapped = query
            .OrderByDescending(m => m.FechaMovimiento)
            .Select(m =>
            {
                var activo = _context.Activos.FirstOrDefault(a => a.Id == m.ActivoId);
                var nombreActivo = activo is null ? "Activo" : $"{activo.CodigoPatrimonial} / {activo.SerialNumber}";
                return new MovimientoDto(m.Id, m.ActivoId, nombreActivo, m.TipoMovimiento, m.DescripcionDetallada, m.FechaMovimiento, m.PlantaOrigen, m.AreaOrigen, m.Modulo);
            })
            .ToList();

        return Task.FromResult((IReadOnlyCollection<MovimientoDto>)mapped);
    }

    public Task<MovementLog> RegistrarAsync(Guid activoId, TipoMovimiento tipo, string descripcion, ModuloSistema modulo, string planta, string area, Guid? administradorId = null)
    {
        var log = new MovementLog
        {
            ActivoId = activoId,
            AdministradorId = administradorId,
            TipoMovimiento = tipo,
            DescripcionDetallada = descripcion,
            PlantaOrigen = planta,
            AreaOrigen = area,
            Modulo = modulo,
            FechaMovimiento = DateTime.UtcNow
        };

        _context.Movimientos.Add(log);
        return Task.FromResult(log);
    }
}
