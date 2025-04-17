namespace Demusicker.Core;

public interface ITraitement
{
    string Nom { get; }
    Task Executer(string projectRoot, IProgress<int> progress);
    bool PeutExecuter(string projectRoot);
}