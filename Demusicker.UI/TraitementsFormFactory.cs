using Demusicker.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Demusicker.UI;

public class TraitementsFormFactory(IServiceProvider serviceProvider)
{
    public TraitementsForm Create(string projectPath)
    {
        return new TraitementsForm(projectPath, serviceProvider.GetRequiredService<ITraitementLoader>());
    }
}
