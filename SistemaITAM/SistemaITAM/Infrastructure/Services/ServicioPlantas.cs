using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Infrastructure.Services;

public class ServicioPlantas : IServicioPlantas
{
    private readonly ContextoDatosEnMemoria _context;

    public ServicioPlantas(ContextoDatosEnMemoria context)
    {
        _context = context;
    }

    public Task<IReadOnlyCollection<Planta>> GetAllAsync() => Task.FromResult((IReadOnlyCollection<Planta>)_context.Plantas.ToList());

    public Task<Planta?> GetAsync(Guid id) => Task.FromResult(_context.Plantas.FirstOrDefault(p => p.Id == id));

    public Task<Planta> CreateAsync(string nombre)
    {
        var planta = new Planta { Nombre = nombre.ToUpperInvariant(), Activa = true };
        _context.Plantas.Add(planta);
        return Task.FromResult(planta);
    }

    public Task<Planta> UpdateAsync(Guid id, string nombre, bool activa)
    {
        var planta = _context.Plantas.First(p => p.Id == id);
        planta.Nombre = nombre.ToUpperInvariant();
        planta.Activa = activa;
        return Task.FromResult(planta);
    }
}
