using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Interfaces;

namespace SistemaITAM.Infrastructure.Exporters;

public class GeneradorPdfQuest : IGeneradorPdf
{
    public Task<byte[]> GenerarActaAsignacionAsync(AsignacionDto asignacion)
    {
        var bytes = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Text("Acta de Asignación de Activo").SemiBold().FontSize(22).FontColor(Colors.Blue.Darken2);

                page.Content().Column(col =>
                {
                    col.Spacing(12);
                    col.Item().Text($"Código Patrimonial: {asignacion.Activo}").FontSize(14);
                    col.Item().Text($"Usuario: {asignacion.Usuario}").FontSize(14);
                    col.Item().Text($"Planta: {asignacion.Planta} | Área: {asignacion.Area}").FontSize(14);
                    col.Item().Text($"Fecha de asignación: {asignacion.FechaAsignacion:dd/MM/yyyy HH:mm}");
                    col.Item().BorderBottom(1).PaddingBottom(12).Text($"Estado de asignación: {asignacion.Estado}").FontSize(12);
                    col.Item().Text("Esta acta se genera automáticamente y queda registrada en el MovementLog del sistema.").FontSize(11);
                });

                page.Footer().AlignCenter().Text("ITAM v2 - Control corporativo de activos").FontSize(10);
            });
        }).GeneratePdf();

        return Task.FromResult(bytes);
    }
}
