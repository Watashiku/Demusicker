using Demusicker.Core;
using System.Reflection;

namespace Demusicker.Traitements;
public class TraitementLoader : ITraitementLoader
{
    public IReadOnlyList<ITraitement> ChargerTousLesTraitements()
    {
        var traitements = new List<ITraitement>();

        Assembly assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => typeof(ITraitement).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            var traitement = Activator.CreateInstance(type) as ITraitement;
            traitements.Add(traitement!);
        }

        return traitements;
    }
}
