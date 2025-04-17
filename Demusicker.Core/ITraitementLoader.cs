namespace Demusicker.Core;

public interface ITraitementLoader
{
    IReadOnlyList<ITraitement> ChargerTousLesTraitements();
}