namespace Demusicker.Core;

public interface ITraitementService
{
    Task<Etape?> Work(Etape etape, IProgress<int> progress);
}
