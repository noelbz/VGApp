using VGApp.Infrastructure.Models;

namespace VGApp.Infrastructure.Interfaces;
public interface IProductService
{
    // Lägger till en ny produkt.
    bool AddProduct(Product product);
    // Hämtar alla produkter.
    IEnumerable<Product> GetAllProducts();
    // Hämtar en produkt baserat på dess unika Id.
    Product? GetProductById(Guid id);
    // Uppdaterar en befintlig produkt.
    bool UpdateProduct(Product product);
    //Tar bort en produkt baserat på dess unika Id.
    bool DeleteProductById(Guid id);
    // Tar bort en produkt baserat på produktobjektet som skickas in.
    bool DeleteProduct(Product product);

}
