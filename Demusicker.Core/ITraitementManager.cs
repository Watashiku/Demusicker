namespace Demusicker.Core;

public interface ITraitementManager
{
    IReadOnlyList<ITraitement> ChargerTousLesTraitements(string racineDuProjet);
    int? VersionApresExecution(ITraitement t);
    void MettreVersionAJour(int version);
}