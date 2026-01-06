using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class AssetService : IAssetService
{
    private readonly InMemoryDataContext _context;

    public AssetService(InMemoryDataContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyCollection<AssetDto>> GetAsync(AssetFilter filter)
    {
        var query = _context.Activos.AsEnumerable();

        if (filter.Tipo.HasValue)
        {
            query = query.Where(a => a.TipoActivo == filter.Tipo.Value);
        }

        if (filter.Estado.HasValue)
        {
            query = query.Where(a => a.EstadoActivo == filter.Estado.Value);
        }

        if (filter.PlantaId.HasValue)
        {
            query = query.Where(a => a.PlantaId == filter.PlantaId.Value);
        }

        if (filter.AreaId.HasValue)
        {
            query = query.Where(a => a.AreaId == filter.AreaId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(a => a.CodigoPatrimonial.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) ||
                                     a.SerialNumber.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));
        }

        var mapped = query.Select(MapToDto).ToList();
        return Task.FromResult((IReadOnlyCollection<AssetDto>)mapped);
    }

    public Task<AssetDto?> GetByIdAsync(Guid id)
    {
        var asset = _context.Activos.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(asset is null ? null : MapToDto(asset));
    }

    public Task<Activo> CreateAsync(Activo activo)
    {
        _context.Activos.Add(activo);
        return Task.FromResult(activo);
    }

    public Task<Activo?> UpdateEstadoAsync(Guid id, EstadoActivo estado)
    {
        var activo = _context.Activos.FirstOrDefault(a => a.Id == id);
        if (activo is null)
        {
            return Task.FromResult<Activo?>(null);
        }

        activo.EstadoActivo = estado;
        return Task.FromResult<Activo?>(activo);
    }

    private AssetDto MapToDto(Activo activo)
    {
        var planta = _context.Plantas.First(p => p.Id == activo.PlantaId);
        var area = _context.Areas.First(a => a.Id == activo.AreaId);
        return new AssetDto(activo.Id, activo.CodigoPatrimonial, activo.SerialNumber, activo.Marca, activo.Modelo, activo.TipoActivo,
            activo.EstadoActivo, activo.PlantaId, activo.AreaId, planta.Nombre, area.Nombre, activo.Observaciones, activo.FechaAlta);
    }
}
