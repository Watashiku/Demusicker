namespace Demusicker;

partial class EntryForm
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
        this.labelRoot = new System.Windows.Forms.Label();
        this.labelPath = new System.Windows.Forms.Label();
        this.buttonBrowse = new System.Windows.Forms.Button();
        this.buttonNewProject = new System.Windows.Forms.Button();
        this.panelProjects = new System.Windows.Forms.FlowLayoutPanel();
        this.SuspendLayout();
        // 
        // labelRoot
        // 
        this.labelRoot.AutoSize = true;
        this.labelRoot.Location = new System.Drawing.Point(12, 15);
        this.labelRoot.Name = "labelRoot";
        this.labelRoot.Size = new System.Drawing.Size(153, 15);
        this.labelRoot.TabIndex = 0;
        this.labelRoot.Text = "Fichier racine des projets :";
        // 
        // labelPath
        // 
        this.labelPath.AutoSize = true;
        this.labelPath.Location = new System.Drawing.Point(12, 40);
        this.labelPath.Name = "labelPath";
        this.labelPath.Size = new System.Drawing.Size(290, 15);
        this.labelPath.TabIndex = 1;
        this.labelPath.Text = "C:\\Users\\adelb\\Demusicker"; // valeur par défaut
        // 
        // buttonBrowse
        // 
        this.buttonBrowse.Location = new System.Drawing.Point(500, 35);
        this.buttonBrowse.Name = "buttonBrowse";
        this.buttonBrowse.Size = new System.Drawing.Size(85, 25);
        this.buttonBrowse.TabIndex = 2;
        this.buttonBrowse.Text = "Parcourir...";
        this.buttonBrowse.UseVisualStyleBackColor = true;
        this.buttonBrowse.Click += new System.EventHandler(this.BrowseButton_Click);
        // 
        // buttonNewProject
        // 
        this.buttonNewProject.Location = new System.Drawing.Point(390, 35);
        this.buttonNewProject.Name = "buttonNewProject";
        this.buttonNewProject.Size = new System.Drawing.Size(100, 25);
        this.buttonNewProject.TabIndex = 3;
        this.buttonNewProject.Text = "Nouveau projet";
        this.buttonNewProject.UseVisualStyleBackColor = true;
        this.buttonNewProject.Click += new System.EventHandler(this.NewProjectButton_Click);
        // 
        // panelProjects
        // 
        this.panelProjects.AutoScroll = true;
        this.panelProjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.panelProjects.Location = new System.Drawing.Point(12, 70);
        this.panelProjects.Name = "panelProjects";
        this.panelProjects.Size = new System.Drawing.Size(573, 300);
        this.panelProjects.TabIndex = 4;
        // 
        // EntryForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.ClientSize = new System.Drawing.Size(600, 390);
        this.Controls.Add(this.panelProjects);
        this.Controls.Add(this.buttonNewProject);
        this.Controls.Add(this.buttonBrowse);
        this.Controls.Add(this.labelPath);
        this.Controls.Add(this.labelRoot);
        this.Name = "EntryForm";
        this.Text = "Demusicker";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label labelRoot;
    private System.Windows.Forms.Label labelPath;
    private System.Windows.Forms.Button buttonBrowse;
    private System.Windows.Forms.Button buttonNewProject;
    private System.Windows.Forms.FlowLayoutPanel panelProjects;
}
