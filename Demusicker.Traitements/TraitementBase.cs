using Demusicker.Core;

namespace Demusicker.Traitements;

internal abstract class TraitementBase : ITraitement
{
    public TraitementBase(string racineDuProjet)
    {
        this.racineDuProjet = racineDuProjet;
        var dossierDeTraitement = Path.Combine(racineDuProjet, Nom);
        if (!Path.Exists(dossierDeTraitement))
        {
            Directory.CreateDirectory(dossierDeTraitement);
        }
    }
    protected string racineDuProjet;
    protected abstract Task<bool> ExecuterInterne(IProgress<int> progress);
    public int? DerniereVersion()
    {
        return ReadVersion();
    }

    public async Task<bool> Executer(IProgress<int> progress, int version)
    {
        Running = true;
        var ok = await ExecuterInterne(progress);
        if (ok)
        {
            WriteVersion(version);
        }
        Running = false;
        return ok;
    }

    public virtual IEnumerable<Type> Dependences => [];
    public string Nom => GetType().Name;
    public bool Running { get; private set; }

    private void WriteVersion(int version)
    {
        var cheminDuFichierDeVersion = Path.Combine(racineDuProjet, Nom, "version");
        File.WriteAllText(cheminDuFichierDeVersion, $"{version}");
    }

    private int? ReadVersion()
    {
        var cheminDuFichierDeVersion = Path.Combine(racineDuProjet, Nom, "version");
        if (!File.Exists(cheminDuFichierDeVersion))
            return null;

        var version = File.ReadAllText(cheminDuFichierDeVersion);

        return int.Parse(version);
    }
}
