using Demusicker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Demusicker;

public partial class TraitementsForm : Form
{
    private readonly string projectPath;
    private readonly ITraitementLoader traitementLoader;
    private readonly Dictionary<ITraitement, Button> boutons = [];

    public TraitementsForm(string projectPath, ITraitementLoader traitementLoader)
    {
        InitializeComponent();
        this.projectPath = projectPath;
        this.traitementLoader = traitementLoader;
        InitTraitementUI();
    }

    private void UpdateTraitementUI()
    {
        foreach (var (t, b) in boutons)
        {
            b.Enabled = t.PeutExecuter(projectPath);
        }
    }

    private void InitTraitementUI()
    {
        var traitements = traitementLoader.ChargerTousLesTraitements();

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
                button.Enabled = false;
                await t.Executer(projectPath, new Progress<int>(v => progress.Value = v));
                progress.Value = 100;
                UpdateTraitementUI();
            };
            UpdateTraitementUI();
        }
    }
}