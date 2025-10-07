using VGApp.Infrastructure.Models;

namespace VGApp.Infrastructure.Interfaces;

public interface IRepositories
{
    Task SaveToFile(IEnumerable<Product> products, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> LoadFromFile(CancellationToken cancellationToken);
}
