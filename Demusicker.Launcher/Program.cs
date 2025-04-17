using Demusicker.Core;
using Demusicker.Traitements;
using Demusicker.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Demusicker.Launcher;

internal static class Program
{
    [STAThread]
    static void Main() 
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();
        ConfigureServices(services);

        var provider = services.BuildServiceProvider();
        Application.Run(provider.GetRequiredService<EntryForm>());
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Traitement
        services.AddSingleton<ITraitementLoader, TraitementLoader>();

        // UI
        services.AddTransient<EntryForm>();
        services.AddTransient<ITraitementLoader, TraitementLoader>();
        services.AddTransient<TraitementsFormFactory>();
    }
}
