namespace VGApp.Infrastructure.Models;
// Representerar produktens tillverkare
public class Manufacturer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}