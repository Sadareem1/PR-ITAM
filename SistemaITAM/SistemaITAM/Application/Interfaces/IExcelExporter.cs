using SistemaITAM.Application.DTOs;

namespace SistemaITAM.Application.Interfaces;

public interface IExcelExporter
{
    Task<byte[]> ExportarActivosAsync(IEnumerable<AssetDto> activos);
}
