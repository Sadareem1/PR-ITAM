using System.Security.Cryptography;
using System.Text;
using SistemaITAM.Domain.Entities;
using SistemaITAM.Domain.Enums;

namespace SistemaITAM.Infrastructure.Services;

public class ContextoDatosEnMemoria
{
    public List<Planta> Plantas { get; } = [];
    public List<Area> Areas { get; } = [];
    public List<Usuario> Usuarios { get; } = [];
    public List<Activo> Activos { get; } = [];
    public List<Asignacion> Asignaciones { get; } = [];
    public List<Administrador> Administradores { get; } = [];
    public List<MovementLog> Movimientos { get; } = [];

    public ContextoDatosEnMemoria()
    {
        SeedPlantas();
        SeedAreas();
        SeedUsuarios();
        SeedActivos();
        SeedAdministradores();
        SeedMovimientos();
    }

    private void SeedPlantas()
    {
        var nombres = new[] { "TRUJILLO", "PIURA", "AREQUIPA", "CUSCO", "NARANJAL", "LURIN", "PISCO" };
        foreach (var nombre in nombres)
        {
            Plantas.Add(new Planta { Nombre = nombre, Activa = true });
        }
    }

    private void SeedAreas()
    {
        var areas = new[] { "Operaciones", "Soporte", "Seguridad", "Finanzas", "Proyectos" };
        foreach (var area in areas)
        {
            Areas.Add(new Area { Nombre = area, Activa = true });
        }
    }

    private void SeedUsuarios()
    {
        var rnd = new Random(2024);
        foreach (var planta in Plantas)
        {
            foreach (var area in Areas)
            {
                Usuarios.Add(new Usuario
                {
                    Nombres = $"Usuario {area.Nombre}",
                    Apellidos = planta.Nombre,
                    DNI = rnd.Next(10000000, 99999999).ToString(),
                    PlantaId = planta.Id,
                    AreaId = area.Id,
                    Estado = EstadoUsuario.Activo
                });
            }
        }
    }

    private void SeedActivos()
    {
        var tipos = Enum.GetValues<TipoActivo>();
        var rnd = new Random(2025);
        for (var i = 0; i < 25; i++)
        {
            var planta = Plantas[rnd.Next(Plantas.Count)];
            var area = Areas[rnd.Next(Areas.Count)];
            var tipo = tipos[rnd.Next(tipos.Length)];
            Activos.Add(new Activo
            {
                CodigoPatrimonial = $"ACT-{1000 + i}",
                SerialNumber = Guid.NewGuid().ToString()[..8].ToUpperInvariant(),
                Marca = "Corporativo",
                Modelo = $"Modelo-{i}",
                TipoActivo = tipo,
                EstadoActivo = EstadoActivo.Disponible,
                PlantaId = planta.Id,
                AreaId = area.Id,
                Observaciones = "Inventario inicial",
                FechaAlta = DateTime.UtcNow.AddDays(-rnd.Next(200))
            });
        }
    }

    private void SeedAdministradores()
    {
        var passwordHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("admin2025")));
        Administradores.Add(new Administrador
        {
            Username = "admin",
            PasswordHash = passwordHash,
            Rol = RolAdministrador.SuperAdmin,
            Activo = true
        });
    }

    private void SeedMovimientos()
    {
        foreach (var activo in Activos.Take(8))
        {
            var planta = Plantas.First(p => p.Id == activo.PlantaId);
            var area = Areas.First(a => a.Id == activo.AreaId);
            Movimientos.Add(new MovementLog
            {
                ActivoId = activo.Id,
                TipoMovimiento = TipoMovimiento.Creacion,
                DescripcionDetallada = "Alta inicial del inventario",
                PlantaOrigen = planta.Nombre,
                AreaOrigen = area.Nombre,
                Modulo = ModuloSistema.Activos,
                FechaMovimiento = activo.FechaAlta
            });
        }
    }
}
