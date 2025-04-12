using Demusicker.Core;

namespace Demusicker.UI;

public class FileSelector : IFileSelector
{
    private readonly string[] fileTypes = ["mp3", "wav", "flac"];
    public string? SelectFile()
    {
        using var dialog = new OpenFileDialog();
        var filter = string.Join(";", fileTypes.Select(x => $"*.{x}"));
        dialog.Filter = $"AudioFiles ({filter})|{filter}";
        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }
}