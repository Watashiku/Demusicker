using System.Diagnostics;
using System.Text;

namespace Demusicker.Traitements;

internal class TraitementStems(string racineDuProjet) : TraitementBase(racineDuProjet)
{
    public override IEnumerable<Type> Dependences => [typeof(TraitementMock2)];
    protected override async Task<bool> ExecuterInterne(IProgress<int> progress)
    {
        var fichierSource = Path.Combine(racineDuProjet, "sourceFile.mp3");
        var dossierDeSolution = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..");

        var executablePython = Path.Combine(dossierDeSolution, "Demusicker.Python", "venv", "Scripts", "python.exe");
        var cheminDuScript = Path.Combine(dossierDeSolution, "Demusicker.Python", "spleeter_separate.py");
        var cheminDeSortie = Path.Combine(racineDuProjet, Nom);
        var arguments = $@"""{fichierSource}"" ""{cheminDeSortie}""";

        if (!File.Exists(cheminDuScript))
        {
            return false;
        }

        var psi = new ProcessStartInfo
        {
            FileName = executablePython, // ou chemin complet vers python.exe si besoin
            Arguments = $"\"{cheminDuScript}\" {arguments}",
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };

        using var process = new Process { StartInfo = psi };
        var errorBuilder = new StringBuilder();

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
                errorBuilder.AppendLine(e.Data);
        };

        process.Start();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        var error = errorBuilder.ToString();
        if (!string.IsNullOrWhiteSpace(error))
        {
            Debug.WriteLine(error);
        }
        return true;
    }
}
