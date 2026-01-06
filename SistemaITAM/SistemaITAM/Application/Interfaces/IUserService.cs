using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Filters;
using SistemaITAM.Domain.Entities;

namespace SistemaITAM.Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyCollection<UserDto>> GetAsync(UserFilter filter);
    Task<int> CountAsync(UserFilter filter);
    Task<IReadOnlyCollection<UserByAreaDto>> GetTotalsByAreaAsync(Guid? plantaId = null);
    Task<Usuario> CreateAsync(Usuario usuario);
}
