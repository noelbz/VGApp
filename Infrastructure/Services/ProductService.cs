using VGApp.Infrastructure.Interfaces;
using VGApp.Infrastructure.Models;

namespace VGApp.Infrastructure.Services;
// Logik för produkthantering

public class ProductService : IProductService
{
    // Fält som kommer lagra repositoryt.
    private readonly IFileRepository _fileRepository;
    // Produktlista
    private List<Product> _products = new List<Product>();

    // Skapar klassen produktservice och injecterar klassen med repositoriet.
    // ProductService följer Interfacet "IProductService" kontrakt.
    public ProductService(IFileRepository fileRepository)
    {
        // Repositoryt sätts till det här fältet,
        // vi kan nu använda dess metoder i denna klass.
        _fileRepository = fileRepository;
        // Skapar en produktlista och fyller den med innehållet från JSON-filen.
        _products = new List<Product>(_fileRepository.Read());
    }

    public bool AddProduct(Product product)
    {
        // Kollar att produkten inte är null, annars avbryt.
        // Kollar att produktens namn inte är tom, annars avbryt.
        if (product == null || string.IsNullOrEmpty(product.Name))
            return false;
        // Kontrollerar om det finns dubletter och sparar dom till "product_exists".
        var product_exists = _products.Any(p => p.Equals(product));
        // Om dubletter finns returnera false.
        if (!product_exists)
        {
            // Ger unikt Id och lägger till i listan.
            product.Id = Guid.NewGuid();
            _products.Add(product);
            // Returnerar en JSON-Fil av produktlistan.
            return _fileRepository.Write(_products);
        }
        // Om det finns dubletter, avbryt.
        return false;
    }
    // Hämtar produktlistan.
    public IEnumerable<Product> GetAllProducts()
    {
        // Läser JSON filen till en produktlista.
        _products = new List<Product>(_fileRepository.Read());
        // Returnerar den nya produktlistan.
        return _products;
    }
    // Hämtar specefik produkt efter ID.
    public Product? GetProductById(Guid id)
    {
        // Läser JSON filen till en produktlista.
        _products = new List<Product>(_fileRepository.Read());
        // Returnerar produkter som 
        return _products.FirstOrDefault(p => p.Id == id);

    }
    // Updaterar produkterna.
    public bool UpdateProduct(Product product)
    {
        // Tar fram produkten via ID.
        var existing_product = GetProductById(product.Id);
        // Om produkten inte är nulll så...
        if (existing_product is not null)
        {
            // Sätt produktens egenskaper.
            existing_product.Name = product.Name;
            existing_product.Price = product.Price;
            existing_product.Category = product.Category;
            existing_product.Manufacturer = product.Manufacturer;
            // Kollar att produkterna inte har samma namn, annars avbryt.
            if (_products.Any(p => p.Id != product.Id && p.Equals(product)))
                return false;
            // Om det finns inga dubletter, skriv produktlistan till JSON-fil.
            return _fileRepository.Write(_products);
        }
        // Om produkten är null så avbryt.
        return false;
    }
    // Tar bort produkt baserat på id.
    public bool DeleteProductById(Guid id)
    {
        // Hämtar produkten via ID.
        var existing_product = GetProductById(id);
        // Om produkten finns och inte är null...
        if (existing_product is not null)
        {
            // Så ta bort produkten från produktlistan.
            _products.Remove(existing_product);
            // Skriv sen resterande produktlistan till JSON-Fil.
            return _fileRepository.Write(_products);
        }
        // Om produkten är null så avbryt.
        return false;
    }
    // Tar bort det givna produktobjektet från listan.
    public bool DeleteProduct(Product product)
    {
        // Den produkt som blir borttaged sparas i "removed" variabeln.
        var removed = _products.Remove(product);
        // Om inget är borttaget så avbryt.
        if (!removed)
            return false;
        // Annars skriv produktlistan till JSON-Fil.
        return _fileRepository.Write(_products);
    }
}
