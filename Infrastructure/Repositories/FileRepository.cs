using System.Text.Json;
using VGApp.Infrastructure.Models;
using VGApp.Infrastructure.Interfaces;

namespace VGApp.Infrastructure.Repositories;
// Logik för filhanteringen.
public class FileRepository : IFileRepository
{
    // Skapa filvägen för vår JSON-fil.
    private readonly string _filePath;
    // Skapa skriv inställningar för JSON-serialisering.
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        WriteIndented = true
    };
    // Konstruktor som sätter upp filvägen och skapar nödvändiga kataloger/filer.
    public FileRepository(string filename = "data.json")
    {
        // Baskatalogen där applikationen körs.
        var baseDir = AppContext.BaseDirectory;
        // Skapar sökvägen till "Data"-katalogen.
        var dataDir = Path.Combine(baseDir, "Data");
        // Fullständiga sökvägen till JSON-filen.
        _filePath = Path.Combine(dataDir, filename);
        // Säkerställer att katalogen och filen finns på plats.
        EnsureInitialized(dataDir, _filePath);
    }
    // Säkerställer att "Data"-Katalogen och filen finns, annars skapas de.
    public static void EnsureInitialized(string dataDir, string _filePath)
    {
        // Om "Data"-katalogen inte finns, skapas den.
        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);
        // Om filen inte finns, skapas en tom JSON-array i filen på angiven filväg.
        if (!File.Exists(_filePath))
            File.WriteAllText(_filePath, "[]");
    }
    // Läser produktlistan från JSON-fil och returnerar en lista med produkter.
    public IEnumerable<Product> Read()
    {
        // Skapar en filström för att läsa från filen.
        using var stream = File.OpenRead(_filePath);
        // Här deserialiseras JSON-innehållet från filströmmen till en lista av Product lista.
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(stream, _jsonSerializerOptions);
        // Returnera produktlistan, om null så returnera en tom array.
        // Enkel lösning och ingen try och catch behövs här.
        return products ?? [];
    }
    // Skriver produktlistan som blir serialiserad till JSON-fil och returnerar true om lyckas.
    public bool Write(List<Product> products)
    {
        try
        {
            using var stream = File.Create(_filePath);
            JsonSerializer.Serialize(stream, products, _jsonSerializerOptions);
            return true;
        }
        catch
        {
            Console.WriteLine("Kunde inte skriva till filen.");
            return false;
        }
    }
}
