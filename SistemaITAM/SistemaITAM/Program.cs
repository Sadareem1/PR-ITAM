using MudBlazor.Services;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Components;
using SistemaITAM.Infrastructure.Exporters;
using SistemaITAM.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddSingleton<ContextoDatosEnMemoria>();
builder.Services.AddScoped<IServicioPlantas, ServicioPlantas>();
builder.Services.AddScoped<IServicioAreas, ServicioAreas>();
builder.Services.AddScoped<IServicioUsuarios, ServicioUsuarios>();
builder.Services.AddScoped<IServicioActivos, ServicioActivos>();
builder.Services.AddScoped<IServicioMovimientos, ServicioMovimientos>();
builder.Services.AddScoped<IServicioAsignaciones, ServicioAsignaciones>();
builder.Services.AddScoped<IServicioDashboard, ServicioDashboard>();
builder.Services.AddScoped<IExportadorExcel, ExportadorExcelClosedXml>();
builder.Services.AddScoped<IGeneradorPdf, GeneradorPdfQuest>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
