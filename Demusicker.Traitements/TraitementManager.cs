using Demusicker.Core;
using System.Data;
using System.Reflection;

namespace Demusicker.Traitements;

public class TraitementManager : ITraitementManager
{
    private readonly Dictionary<Type, ITraitement> _instances = [];
    private int version = 0;

    public void MettreVersionAJour(int version) => this.version = Math.Max(version, this.version);

    public int? VersionApresExecution(ITraitement t)
    {
        if (t.Running)
        {
            return null;
        }

        if (!t.Dependences.Any())
        {
            return version + 1;
        }

        var versionDependances = t.Dependences
            .Select(depType => _instances.TryGetValue(depType, out var dep) ? dep : throw new InvalidOperationException($"Dépendance manquante : {depType.Name}"))
            .Select(dep => dep.DerniereVersion() ?? -1)
            .Distinct()
            .ToList();

        if (versionDependances.Count != 1 || versionDependances.Single() == -1)
        {
            return null;
        }
        if (versionDependances.Single() == version && t.DerniereVersion() != version)
        {
            return version;
        }
        return version + 1;
    }

    public IReadOnlyList<ITraitement> ChargerTousLesTraitements(string racineDuProjet)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => typeof(TraitementBase).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            var t = (Activator.CreateInstance(type, [racineDuProjet]) as ITraitement)!;
            version = Math.Max(version, t.DerniereVersion() ?? 0);
            _instances[type] = t;
        }

        return TrierParOrdreExecution();
    }

    private List<ITraitement> TrierParOrdreExecution()
    {
        var visited = new HashSet<Type>();
        var loop = new HashSet<Type>();
        var result = new List<ITraitement>();

        void Visit(Type type)
        {
            if (visited.Contains(type))
                return;

            if (!_instances.TryGetValue(type, out ITraitement? traitement))
                throw new InvalidOperationException($"Dépendance manquante : {type.Name}");

            if (loop.Contains(type))
                throw new InvalidOperationException($"Dépendance circulaire : {type.Name}");
            loop.Add(type);

            foreach (var depType in traitement.Dependences)
                Visit(depType);

            loop.Remove(type);
            visited.Add(type);
            result.Add(traitement);
        }

        foreach (var type in _instances.Keys)
            Visit(type);

        return result;
    }
}
