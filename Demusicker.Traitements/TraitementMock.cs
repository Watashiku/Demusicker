namespace Demusicker.Traitements;

internal class TraitementMock(string racineDuProjet) : TraitementBase(racineDuProjet)
{
    protected override async Task<bool> ExecuterInterne(IProgress<int> progress)
    {
        for (var i = 0; i < 100; i++)
        {
            progress.Report(i);
            await Task.Delay(20);
        }
        return true;
    }
}
