using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Domain.Entities;

public class Administrador
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RolAdministrador Rol { get; set; } = RolAdministrador.SuperAdmin;
    public bool Activo { get; set; } = true;
}
