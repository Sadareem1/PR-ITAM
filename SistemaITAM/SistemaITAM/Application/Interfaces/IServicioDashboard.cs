using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IServicioDashboard
{
    Task<ResumenDashboardDto> BuildAsync();
}
