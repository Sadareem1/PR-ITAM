using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly InMemoryDataContext _context;

    public UserService(InMemoryDataContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyCollection<UserDto>> GetAsync(UserFilter filter)
    {
        var query = _context.Usuarios.AsEnumerable();

        if (filter.PlantaId.HasValue)
        {
            query = query.Where(u => u.PlantaId == filter.PlantaId.Value);
        }

        if (filter.AreaId.HasValue)
        {
            query = query.Where(u => u.AreaId == filter.AreaId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(u => u.NombreCompleto.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) || u.DNI.Contains(filter.Search));
        }

        var mapped = query.Select(u =>
        {
            var planta = _context.Plantas.First(p => p.Id == u.PlantaId);
            var area = _context.Areas.First(a => a.Id == u.AreaId);
            return new UserDto(u.Id, u.Nombres, u.Apellidos, u.DNI, u.PlantaId, u.AreaId, planta.Nombre, area.Nombre, u.Estado);
        }).ToList();

        return Task.FromResult((IReadOnlyCollection<UserDto>)mapped);
    }

    public async Task<int> CountAsync(UserFilter filter)
    {
        var users = await GetAsync(filter);
        return users.Count;
    }

    public Task<IReadOnlyCollection<UserByAreaDto>> GetTotalsByAreaAsync(Guid? plantaId = null)
    {
        var query = _context.Usuarios.AsEnumerable();
        if (plantaId.HasValue)
        {
            query = query.Where(u => u.PlantaId == plantaId.Value);
        }

        var totals = query
            .GroupBy(u => new { u.AreaId, u.PlantaId })
            .Select(g =>
            {
                var area = _context.Areas.First(a => a.Id == g.Key.AreaId);
                var planta = _context.Plantas.First(p => p.Id == g.Key.PlantaId);
                return new UserByAreaDto(area.Nombre, planta.Nombre, g.Count());
            })
            .ToList();

        return Task.FromResult((IReadOnlyCollection<UserByAreaDto>)totals);
    }

    public Task<Usuario> CreateAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        return Task.FromResult(usuario);
    }
}
