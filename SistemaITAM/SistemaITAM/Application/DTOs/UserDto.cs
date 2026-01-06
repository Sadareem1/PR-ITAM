using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Application.DTOs;

public record UserDto(
    Guid Id,
    string Nombres,
    string Apellidos,
    string DNI,
    Guid PlantaId,
    Guid AreaId,
    string Planta,
    string Area,
    EstadoUsuario Estado);

public record UserByAreaDto(string Area, string Planta, int TotalUsuarios);
