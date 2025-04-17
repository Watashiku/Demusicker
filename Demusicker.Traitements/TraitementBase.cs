using Demusicker.Core;

namespace Demusicker.Traitements;

internal abstract class TraitementBase : ITraitement
{
    private bool running = false;
    protected abstract Task ExecuterInterne(string projectRoot, IProgress<int> progress);
    protected abstract bool PeutExecuterInterne(string projectRoot);

    public virtual IEnumerable<Type> Dependencies => [];
    public string Nom => GetType().Name;

    public async Task Executer(string projectRoot, IProgress<int> progress)
    {
        running = true;
        await ExecuterInterne(projectRoot, progress);
        running = false;
    }


    public bool PeutExecuter(string projectRoot)
    {
        return !running && PeutExecuterInterne(projectRoot);
    }
}
