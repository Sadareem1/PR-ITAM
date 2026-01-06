using System.Collections.Concurrent;
using SistemaITAM.Application.DTOs;
using SistemaITAM.Application.Interfaces;
using SistemaITAM.Domain.Entities;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class AssignmentService : IAssignmentService
{
    private readonly InMemoryDataContext _context;
    private readonly IMovementLogService _movementLogService;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public AssignmentService(InMemoryDataContext context, IMovementLogService movementLogService, IPdfGenerator pdfGenerator)
    {
        _context = context;
        _movementLogService = movementLogService;
        _pdfGenerator = pdfGenerator;
    }

    public Task<IReadOnlyCollection<AssignmentDto>> GetAsync()
    {
        var mapped = _context.Asignaciones.Select(MapToDto).ToList();
        return Task.FromResult((IReadOnlyCollection<AssignmentDto>)mapped);
    }

    public async Task<AssignmentResultDto> AssignAsync(AssignmentRequestDto request)
    {
        await _semaphore.WaitAsync();
        try
        {
            var activo = _context.Activos.FirstOrDefault(a => a.Id == request.ActivoId) ?? throw new InvalidOperationException("Activo no encontrado");
            if (activo.EstadoActivo != EstadoActivo.Disponible)
            {
                throw new InvalidOperationException("El activo no está disponible para asignar");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == request.UsuarioId) ?? throw new InvalidOperationException("Usuario no encontrado");
            var administrador = _context.Administradores.FirstOrDefault(a => a.Id == request.AdministradorId) ?? throw new InvalidOperationException("Administrador no encontrado");

            var asignacion = new Asignacion
            {
                ActivoId = activo.Id,
                UsuarioId = usuario.Id,
                AdministradorId = administrador.Id,
                FechaAsignacion = DateTime.UtcNow,
                EstadoAsignacion = EstadoAsignacion.Activa,
                RutaPdfActa = string.Empty
            };

            _context.Asignaciones.Add(asignacion);
            activo.EstadoActivo = EstadoActivo.Asignado;

            var area = _context.Areas.First(a => a.Id == activo.AreaId);
            var planta = _context.Plantas.First(p => p.Id == activo.PlantaId);

            await _movementLogService.RegistrarAsync(activo.Id, TipoMovimiento.Asignacion, $"Asignado a {usuario.NombreCompleto} por {administrador.Username}", ModuloSistema.Asignaciones, planta.Nombre, area.Nombre, administrador.Id);

            var dto = MapToDto(asignacion);
            var pdf = await _pdfGenerator.GenerarActaAsignacionAsync(dto);
            asignacion.RutaPdfActa = $"/actas/{asignacion.Id}.pdf";

            return new AssignmentResultDto(dto, pdf);
        }
        catch
        {
            // rollback in-memory
            var asignacion = _context.Asignaciones.FirstOrDefault(a => a.ActivoId == request.ActivoId && a.UsuarioId == request.UsuarioId);
            if (asignacion is not null)
            {
                _context.Asignaciones.Remove(asignacion);
            }

            var activo = _context.Activos.FirstOrDefault(a => a.Id == request.ActivoId);
            if (activo is not null)
            {
                activo.EstadoActivo = EstadoActivo.Disponible;
            }

            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private AssignmentDto MapToDto(Asignacion asignacion)
    {
        var activo = _context.Activos.First(a => a.Id == asignacion.ActivoId);
        var usuario = _context.Usuarios.First(u => u.Id == asignacion.UsuarioId);
        var planta = _context.Plantas.First(p => p.Id == activo.PlantaId);
        var area = _context.Areas.First(a => a.Id == activo.AreaId);
        return new AssignmentDto(asignacion.Id, asignacion.ActivoId, asignacion.UsuarioId, asignacion.AdministradorId, asignacion.FechaAsignacion, asignacion.EstadoAsignacion, asignacion.RutaPdfActa, activo.CodigoPatrimonial, usuario.NombreCompleto, planta.Nombre, area.Nombre);
    }
}
