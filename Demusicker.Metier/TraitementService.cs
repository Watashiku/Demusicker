using Demusicker.Core;
using System.Diagnostics;
using System.Text;

namespace Demusicker.Metier;

public class TraitementService(IFileSelector _fileSelector, IErrorLogger _logger) : ITraitementService
{
    private FileInfo? sourceFile;

    public async Task<Etape?> Work(Etape etape, IProgress<int> progress)
    {
        progress.Report((int)etape*10);
        return etape switch
        {
            Etape.Init => ChoisirFichier(),
            Etape.Fichier => await CreerLesStems(),
            Etape.Stem => TrouverLesMidis(),
            Etape.Midi => TrouverLesPresets(),
            Etape.Preset => CreerLeFlp(),
            Etape.Flp => OuvrirLeFlp(),
            _ => throw new ArgumentOutOfRangeException(nameof(etape), $"Étape non gérée : {etape}")
        };
    }

    private Etape? ChoisirFichier()
    {
        var fileName = _fileSelector.SelectFile();
        if (fileName is null) return null;

        var newFile = new FileInfo(fileName);
        if (newFile is null || newFile == sourceFile) return null;

        sourceFile = newFile;
        return Etape.Fichier;
    }

    private async Task<Etape?> CreerLesStems()
    {
        if (sourceFile is null) return null;
        var solutionFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..");

        var pythonExe = Path.Combine(solutionFolder, "Demusicker.Python", "venv", "Scripts", "python.exe");
        var scriptPath = Path.Combine(solutionFolder, "Demusicker.Python", "spleeter_separate.py");
        var outputPath = @"C:\Temp\stems";
        var arguments = $@"""{sourceFile.FullName}"" ""{outputPath}""";

        if (!File.Exists(scriptPath))
        {
            _logger.DisplayError($"Script Python introuvable : {scriptPath}");
            return null;
        }

        var psi = new ProcessStartInfo
        {
            FileName = pythonExe, // ou chemin complet vers python.exe si besoin
            Arguments = $"\"{scriptPath}\" {arguments}",
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
            _logger.DisplayError($"Erreur lors de l'exécution du script : {error}");
            return null;
        }

        return Etape.Stem;
    }

    private Etape TrouverLesMidis()
    {
        return Etape.Midi;
    }

    private Etape TrouverLesPresets()
    {
        return Etape.Preset;
    }

    private Etape CreerLeFlp()
    {
        return Etape.Flp;
    }

    private Etape? OuvrirLeFlp()
    {
        return null;
    }
}
