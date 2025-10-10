using VGApp.Infrastructure.Models;

namespace VGApp.Infrastructure.Interfaces;
public interface IFileRepository
{
    // Skriver produktlista till fil.
    bool Write(List<Product> products);
    // Läser produktlista från fil.
    IEnumerable<Product> Read();
}
