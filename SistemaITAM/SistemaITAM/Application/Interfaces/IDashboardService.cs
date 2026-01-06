using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> BuildAsync();
}
