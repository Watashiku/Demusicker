namespace Demusicker;

partial class TraitementsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    #region Code généré par le Concepteur Windows Form

    private void InitializeComponent()
    {
        this.panelTraitements = new System.Windows.Forms.FlowLayoutPanel();
        this.SuspendLayout();
        // 
        // panelTraitements
        // 
        this.panelTraitements.AutoScroll = true;
        this.panelTraitements.Location = new System.Drawing.Point(12, 12);
        this.panelTraitements.Name = "panelTraitements";
        this.panelTraitements.Size = new System.Drawing.Size(560, 400);
        this.panelTraitements.TabIndex = 0;
        // 
        // TraitementForm
        // 
        this.ClientSize = new System.Drawing.Size(584, 431);
        this.Controls.Add(this.panelTraitements);
        this.Name = "TraitementForm";
        this.Text = "Traitements";
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel panelTraitements;
}
