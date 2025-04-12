using Demusicker.Core;
using Demusicker.Metier;
using Microsoft.Extensions.DependencyInjection;

namespace Demusicker.UI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        //ApplicationConfiguration.Initialize();
        //Application.Run(new Form1());


        ApplicationConfiguration.Initialize();
        var services = new ServiceCollection();
        services.AddTransient<ITraitementService, TraitementService>();
        services.AddTransient<IFileSelector, FileSelector>();
        services.AddTransient<IErrorLogger, ErrorLogger>();
        services.AddTransient<MainForm>();

        var provider = services.BuildServiceProvider();
        Application.Run(provider.GetRequiredService<MainForm>());

    }
}