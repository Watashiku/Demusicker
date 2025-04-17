﻿namespace Demusicker.Traitements;

internal class TraitementMock : TraitementBase
{
    public override IEnumerable<Type> Dependencies => [typeof(TraitementMock2)];

    protected override async Task ExecuterInterne(string projectRoot, IProgress<int> progress)
    {
        for (var i = 0; i < 100; i++)
        {
            progress.Report(i);
            await Task.Delay(20);
        }
    }

    protected override bool PeutExecuterInterne(string projectRoot)
    {
        return true;
    }
}
