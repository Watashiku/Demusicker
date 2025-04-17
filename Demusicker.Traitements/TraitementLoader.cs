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
            .Where(t => typeof(TraitementBase).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            traitements.Add((Activator.CreateInstance(type) as ITraitement)!);
        }

        return TrierParOrdreExecution(traitements);
    }

    public static List<ITraitement> TrierParOrdreExecution(IEnumerable<ITraitement> traitements)
    {
        var traitementDict = traitements.ToDictionary(t => t.GetType());
        var visited = new HashSet<Type>();
        var loop = new HashSet<Type>();
        var result = new List<ITraitement>();

        void Visit(Type type)
        {
            if (visited.Contains(type))
                return;

            if (!traitementDict.TryGetValue(type, out ITraitement? traitement))
                throw new InvalidOperationException($"Dépendance manquante : {type.Name}");

            if (loop.Contains(type))
                throw new InvalidOperationException($"Dépendance circulaire : {type.Name}");
            loop.Add(type);

            foreach (var depType in traitement.Dependencies)
                Visit(depType);

            loop.Remove(type);
            visited.Add(type);
            result.Add(traitement);
        }

        foreach (var type in traitementDict.Keys)
            Visit(type);

        return result;
    }

}
