using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IAssignmentService
{
    Task<IReadOnlyCollection<AssignmentDto>> GetAsync();
    Task<AssignmentResultDto> AssignAsync(AssignmentRequestDto request);
}
