// Importerar biblioteket som behövs för Dependency Injection.
// DI gör det möjligt att hantera tjänster och deras beroenden automatiskt.
using Microsoft.Extensions.DependencyInjection;
// Importerar biblioteket som behövs för Hosting.
using Microsoft.Extensions.Hosting;
// Host är en central plats som hanterar appen och dess tjänster.
var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        // Registrering för alla tjänster som appen behöver,
        // tjänster som fil-hantering och produkt-hantering.

        // Registrerar repositoryt för filhanteringen (DATA ÅTKOMST).
        // AddScoped = Skapar en instans vid varje scope.
        services.AddScoped<IFileRepository, FileRepository>();
        // Skapar en instans vid varje scope.
        // AddScoped = Registrerar tjänsten som hanterar produkter (AFFÄRSLOGIK).
        services.AddScoped<IProductService, ProductService>();

        // Registrerar WPF-startfönstret (UI-FÖNSTER).
        // AddSingleton = Använder samma instans hela tiden.
        services.AddSingleton<MainWindow>();
    })
    // Bygger hosten som kan nu hämta våra tjänster och det vi registrerat.
    .Build();

// Hämtar MainWindow från hostens tjänster.
// Instans av MainWindow med alla beroenden injecterat och sparar det till "mainWindow" variabeln.
var mainWindow = host.Services.GetRequiredService<MainWindow>();
// Skapar instans av WPF-applikationen
var app = new App();
// Kör nu WPF-Applikationen med "Run" metoden som loopar och håller programmet igång,
// tills startfönstret MainWindow stängs.
app.Run(mainWindow);