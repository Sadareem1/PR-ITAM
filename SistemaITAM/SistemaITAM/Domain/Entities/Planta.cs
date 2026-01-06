namespace SistemaITAM.Domain.Entities;

public class Planta
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public bool Activa { get; set; } = true;
}
