namespace VGApp.Infrastructure.Models;
// Representar själva produkten.
// (Id, Namn, Pris, Kategori och Tillverkare).
public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public Category? Category { get; set; }
    public Manufacturer? Manufacturer { get; set; }

    // Hjälpmetod för att klona ett produktobjekt.
    public Product Clone()
    {
        // Returnerar en ny instans Product med samma värden.
        return new Product
        {
            Id = this.Id,
            Name = this.Name,
            Price = this.Price,
            // Om Category eller Manufacturer är null, sätts de till null i klonen.
            // Annars skapas nya instanser med samma Id och Name som originalet.
            Category = this.Category == null ? null : new Category { Id = this.Id, Name = this.Name},
            Manufacturer = this.Manufacturer == null ? null : new Manufacturer { Id = this.Id, Name = this.Name }
        };
    }
    // Jämför produkter efter deras namn och letar efter dubletter.
    public override bool Equals(object? obj)
    {
        if (obj is Product p)
            return string.Equals(this.Name?.Trim(), p.Name?.Trim(), StringComparison.OrdinalIgnoreCase);
        return false;
    }
    public override int GetHashCode() => this.Name?.Trim().ToLowerInvariant().GetHashCode() ?? base.GetHashCode();
}