using Demusicker.UI;
using System;
using System.IO;
using System.Windows.Forms;

namespace Demusicker;

public partial class EntryForm : Form
{
    private string rootPath = @"C:\Users\adelb\Demusicker";
    private readonly TraitementsFormFactory _traitementsFormFactory;

    public EntryForm(TraitementsFormFactory traitementsFormFactory)
    {
        StartPosition = FormStartPosition.CenterScreen;
        _traitementsFormFactory = traitementsFormFactory;
        InitializeComponent();
        labelPath.Text = rootPath;
        LoadProjects();
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
        using var fbd = new FolderBrowserDialog();
        fbd.SelectedPath = rootPath;

        if (fbd.ShowDialog() == DialogResult.OK)
        {
            rootPath = fbd.SelectedPath;
            labelPath.Text = rootPath;
            LoadProjects();
        }
    }

    private void NewProjectButton_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.Filter = "Fichiers audio|*.wav;*.mp3";

        if (ofd.ShowDialog() == DialogResult.OK)
        {
            string audioFilePath = ofd.FileName;
            string projectName = Path.GetFileNameWithoutExtension(audioFilePath);
            string projectDir = Path.Combine(rootPath, projectName);
            int i = 1;

            while (Directory.Exists(projectDir))
                projectDir = Path.Combine(rootPath, $"{projectName}_{i++}");

            Directory.CreateDirectory(projectDir);

            string destAudioFile = Path.Combine(projectDir, Path.GetFileName(audioFilePath));
            File.Copy(audioFilePath, destAudioFile);

            var state = new
            {
                projectName = Path.GetFileName(projectDir),
                sourceFile = Path.GetFileName(destAudioFile),
                createdAt = DateTime.UtcNow.ToString("o"),
                treatments = new { }
            };

            var statePath = Path.Combine(projectDir, "state.json");
            File.WriteAllText(statePath, System.Text.Json.JsonSerializer.Serialize(state, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));

            LoadProjects();
        }
    }

    private void LoadProjects()
    {
        panelProjects.Controls.Clear();
        if (!Directory.Exists(rootPath)) return;

        foreach (var dir in Directory.GetDirectories(rootPath))
        {
            var stateFile = Path.Combine(dir, "state.json");
            if (!File.Exists(stateFile)) continue;

            var projectName = Path.GetFileName(dir);
            var btn = new Button
            {
                Text = projectName,
                Width = 540,
                Height = 30,
                Tag = dir
            };
            btn.Click += (s, e) =>
            {
                var selectedProjectPath = (string)btn.Tag;
                var traitementForm = _traitementsFormFactory.Create(selectedProjectPath);
                traitementForm.StartPosition = FormStartPosition.Manual;
                traitementForm.Location = Location;
                this.Hide();
                traitementForm.ShowDialog();
                this.Close();
            };
            panelProjects.Controls.Add(btn);
        }
    }
}
