using Demusicker.Core;
using System.Diagnostics;
using System.Text;

namespace Demusicker.Metier;

public class PythonScriptExecutor(IErrorLogger _logger)
{
    public Task<bool> ExecuteStep(Etape etape, FileInfo sourceFile, string outputPath)
    {
        var arguments = $@"""{sourceFile.FullName}"" ""{outputPath}""";
        var scriptName = etape switch
        {
            Etape.Stem => "spleeter_separate.py",
            _ => throw new NotImplementedException(),
        };
        return RunPythonScript(scriptName, arguments);
    }

    // Méthode pour exécuter le script Python
    private async Task<bool> RunPythonScript(string scriptPath, string arguments)
    {
        if (!File.Exists(scriptPath))
        {
            _logger.DisplayError($"Script Python introuvable : {scriptPath}");
            return false;
        }

        var solutionFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..");
        var pythonExe = Path.Combine(solutionFolder, "Demusicker.Python", "venv", "Scripts", "python.exe");
        var scriptFullPath = Path.Combine(solutionFolder, "Demusicker.Python", scriptPath);

        var processStartInfo = new ProcessStartInfo
        {
            FileName = pythonExe,
            Arguments = $"\"{scriptFullPath}\" {arguments}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        using var process = new Process { StartInfo = processStartInfo };
        var outputBuilder = new StringBuilder();
        var errorBuilder = new StringBuilder();

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
                errorBuilder.AppendLine(e.Data);
        };
        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
                outputBuilder.AppendLine(e.Data);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        var output = outputBuilder.ToString();
        Debug.WriteLine(output, "OUTPUT--");
        var error = errorBuilder.ToString();
        if (string.IsNullOrWhiteSpace(error)) return true;

        _logger.DisplayError($"Erreur lors de l'exécution du script : {error}");
        return false;
    }
}
