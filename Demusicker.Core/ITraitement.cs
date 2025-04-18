namespace Demusicker.Core;

public interface ITraitement
{
    public bool Running { get; }
    string Nom { get; }
    Task<bool> Executer(IProgress<int> progress, int version);
    int? DerniereVersion();
    IEnumerable<Type> Dependences { get; }
}