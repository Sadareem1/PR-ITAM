using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record UsuarioDto(
    Guid Id,
    string Nombres,
    string Apellidos,
    string DNI,
    Guid PlantaId,
    Guid AreaId,
    string Planta,
    string Area,
    EstadoUsuario Estado);

public record UsuarioPorAreaDto(string Area, string Planta, int TotalUsuarios);
