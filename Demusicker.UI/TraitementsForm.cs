using Demusicker.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Demusicker.UI;

public partial class TraitementsForm : Form
{
    private readonly string projectPath;
    private readonly ITraitementManager traitementManager;
    private readonly Dictionary<ITraitement, Button> boutons = [];

    public TraitementsForm(string projectPath, ITraitementManager traitementManager)
    {
        InitializeComponent();
        this.projectPath = projectPath;
        this.traitementManager = traitementManager;
        InitTraitementUI();
    }

    private void UpdateTraitementUI()
    {
        foreach (var (t, b) in boutons)
        {
            b.Enabled = traitementManager.VersionApresExecution(t) is not null;
            var dv = t.DerniereVersion();
            if (dv is not null)
            {
                b.BackColor = ButtonColorPalette.Get(dv);
            }
        }
    }

    private void InitTraitementUI()
    {
        var traitements = traitementManager.ChargerTousLesTraitements(projectPath);

        foreach (var t in traitements)
        {
            var panel = new Panel { Width = 540, Height = 60 };

            var button = new Button
            {
                Text = t.Nom,
                Width = 200,
                Height = 30,
                Top = 5,
                Left = 5,
                Enabled = false
            };
            boutons[t] = button;

            var progress = new ProgressBar
            {
                Width = 300,
                Height = 20,
                Top = 10,
                Left = 220
            };

            panel.Controls.Add(button);
            panel.Controls.Add(progress);
            panelTraitements.Controls.Add(panel);

            button.Click += async (s, e) =>
            {
                var version = traitementManager.VersionApresExecution(t);
                if (version is null) return;
                button.Enabled = false;
                var ok = await t.Executer(new Progress<int>(v => progress.Value = v), version.Value);
                if (ok) traitementManager.MettreVersionAJour(version.Value);
                progress.Value = 100;
                UpdateTraitementUI();
            };
        }
        UpdateTraitementUI();
    }
}