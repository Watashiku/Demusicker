using Demusicker.Core;

namespace Demusicker.Traitements;

internal class TraitementMock : ITraitement
{
    public string Nom => "throw new NotImplementedException()";

    public async Task Executer(string projectRoot, IProgress<int> progress)
    {
        for (var i = 0; i < 100; i++) {
            progress.Report(i);
            await Task.Delay(20);
        }
    }

    public bool PeutExecuter(string projectRoot)
    {
        return true;
    }
}
