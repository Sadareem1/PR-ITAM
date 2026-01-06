using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Infrastructure.Services;

public class AreaService : IAreaService
{
    private readonly InMemoryDataContext _context;

    public AreaService(InMemoryDataContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyCollection<Area>> GetAllAsync() => Task.FromResult((IReadOnlyCollection<Area>)_context.Areas.ToList());

    public Task<Area?> GetAsync(Guid id) => Task.FromResult(_context.Areas.FirstOrDefault(a => a.Id == id));

    public Task<Area> CreateAsync(string nombre)
    {
        var area = new Area { Nombre = nombre, Activa = true };
        _context.Areas.Add(area);
        return Task.FromResult(area);
    }

    public Task<Area> UpdateAsync(Guid id, string nombre, bool activa)
    {
        var area = _context.Areas.First(a => a.Id == id);
        area.Nombre = nombre;
        area.Activa = activa;
        return Task.FromResult(area);
    }
}
