using Demusicker.Core;

namespace Demusicker.UI;

public partial class MainForm : Form
{
    private Etape etapeMin = Etape.Init;
    private Etape etapeMax = Etape.Init;

    private readonly Dictionary<Etape, Button> mesBoutons;
    private readonly ITraitementService _traitementService;

    public MainForm(ITraitementService traitementService)
    {
        _traitementService = traitementService;
        InitializeComponent();
        mesBoutons = new Dictionary<Etape, Button>()
        {
            { Etape.Init, FileBtn },
            { Etape.Fichier, StemBtn },
            { Etape.Stem, MidiBtn },
            { Etape.Midi, PresetBtn },
            { Etape.Preset, CreateFlpBtn },
            { Etape.Flp, OpenFlpBtn }
        };
        foreach (var pair in mesBoutons)
        {
            pair.Value.Tag = pair.Key;
            pair.Value.Click += Bouton_Click;
        }
        MettreAJourUI(Etape.Init);
    }

    private void UpdateButtonState(Button button, Etape etapeBouton)
    {
        if (etapeBouton < etapeMin)
        {
            button.Enabled = true;
            button.BackColor = Color.GreenYellow;
        }
        else if (etapeBouton == etapeMin)
        {
            button.Enabled = true;
            button.BackColor = Color.MediumTurquoise;
        }
        else if (etapeBouton <= etapeMax)
        {
            button.Enabled = true;
            button.BackColor = Color.Khaki;
        }
        else
        {
            button.Enabled = false;
            button.BackColor = Color.LightGray;
        }
    }

    private void BloquerUI()
    {
        foreach (var button in mesBoutons.Values)
        {
            button.Enabled = false;
            button.BackColor = Color.WhiteSmoke;
        }
    }

    private void MettreAJourEtapesLimites(Etape etape)
    {
        if (etape > etapeMax) etapeMax = etape;
        if (etape <= etapeMin + 1) etapeMin = etape;
    }

    private void MettreAJourUI(Etape? etape)
    {
        if (etape is not null) MettreAJourEtapesLimites(etape.Value);
        foreach (var pair in mesBoutons) UpdateButtonState(pair.Value, pair.Key);
    }

    private async void Bouton_Click(object? sender, EventArgs e)
    {
        if (sender is Button bouton && bouton.Tag is Etape etape)
        {
            var progress = new Progress<int>(r => ProgressBar.Value = r);
            BloquerUI();
            var nouvelleEtape = await _traitementService.Work(etape, progress);
            MettreAJourUI(nouvelleEtape);
        }
    }
}
