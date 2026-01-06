using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IServicioUsuarios
{
    Task<IReadOnlyCollection<UsuarioDto>> GetAsync(FiltroUsuarios filter);
    Task<int> CountAsync(FiltroUsuarios filter);
    Task<IReadOnlyCollection<UsuarioPorAreaDto>> GetTotalsByAreaAsync(Guid? plantaId = null);
    Task<Usuario> CreateAsync(Usuario usuario);
}
