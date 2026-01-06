using ClosedXML.Excel;
using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Interfaces;

namespace SistemaITAM.Infrastructure.Exporters;

public class ClosedXmlExcelExporter : IExcelExporter
{
    public Task<byte[]> ExportarActivosAsync(IEnumerable<AssetDto> activos)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Inventario");
        var headers = new[] { "Código Patrimonial", "Serie", "Marca", "Modelo", "Tipo", "Estado", "Planta", "Área", "Fecha Alta", "Observaciones" };
        for (int i = 0; i < headers.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = headers[i];
            worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.FromArgb(233, 240, 250);
        }

        var row = 2;
        foreach (var activo in activos)
        {
            worksheet.Cell(row, 1).Value = activo.CodigoPatrimonial;
            worksheet.Cell(row, 2).Value = activo.SerialNumber;
            worksheet.Cell(row, 3).Value = activo.Marca;
            worksheet.Cell(row, 4).Value = activo.Modelo;
            worksheet.Cell(row, 5).Value = activo.TipoActivo.ToString();
            worksheet.Cell(row, 6).Value = activo.Estado.ToString();
            worksheet.Cell(row, 7).Value = activo.Planta;
            worksheet.Cell(row, 8).Value = activo.Area;
            worksheet.Cell(row, 9).Value = activo.FechaAlta;
            worksheet.Cell(row, 10).Value = activo.Observaciones;
            row++;
        }

        worksheet.Columns().AdjustToContents();
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return Task.FromResult(stream.ToArray());
    }
}
