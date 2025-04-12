namespace Demusicker.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StemBtn = new Button();
            FileBtn = new Button();
            MidiBtn = new Button();
            PresetBtn = new Button();
            CreateFlpBtn = new Button();
            OpenFlpBtn = new Button();
            ProgressBar = new ProgressBar();
            SuspendLayout();
            // 
            // StemBtn
            // 
            StemBtn.Location = new Point(142, 12);
            StemBtn.Name = "StemBtn";
            StemBtn.Size = new Size(123, 69);
            StemBtn.TabIndex = 0;
            StemBtn.Text = "Créer les stems";
            StemBtn.UseVisualStyleBackColor = true;
            // 
            // ProgressBar
            // 
            ProgressBar.Location = new Point(12, 87);
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(769, 16);
            ProgressBar.TabIndex = 2;
            // 
            // FileBtn
            // 
            FileBtn.Location = new Point(12, 12);
            FileBtn.Name = "FileBtn";
            FileBtn.Size = new Size(123, 69);
            FileBtn.TabIndex = 3;
            FileBtn.Text = "Choix du fichier";
            FileBtn.UseVisualStyleBackColor = true;
            // 
            // MidiBtn
            // 
            MidiBtn.Location = new Point(271, 12);
            MidiBtn.Name = "MidiBtn";
            MidiBtn.Size = new Size(123, 69);
            MidiBtn.TabIndex = 6;
            MidiBtn.Text = "Trouver les midis";
            MidiBtn.UseVisualStyleBackColor = true;
            // 
            // PresetBtn
            // 
            PresetBtn.Location = new Point(400, 12);
            PresetBtn.Name = "PresetBtn";
            PresetBtn.Size = new Size(123, 69);
            PresetBtn.TabIndex = 7;
            PresetBtn.Text = "Trouver les presets";
            PresetBtn.UseVisualStyleBackColor = true;
            // 
            // CreateFlpBtn
            // 
            CreateFlpBtn.Location = new Point(529, 12);
            CreateFlpBtn.Name = "CreateFlpBtn";
            CreateFlpBtn.Size = new Size(123, 69);
            CreateFlpBtn.TabIndex = 9;
            CreateFlpBtn.Text = "Creer le .flp";
            CreateFlpBtn.UseVisualStyleBackColor = true;
            // 
            // OpenFlpBtn
            // 
            OpenFlpBtn.Location = new Point(658, 12);
            OpenFlpBtn.Name = "OpenFlpBtn";
            OpenFlpBtn.Size = new Size(123, 69);
            OpenFlpBtn.TabIndex = 11;
            OpenFlpBtn.Text = "Ouvrir le .flp";
            OpenFlpBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(794, 115);
            Controls.Add(FileBtn);
            Controls.Add(StemBtn);
            Controls.Add(MidiBtn);
            Controls.Add(PresetBtn);
            Controls.Add(CreateFlpBtn);
            Controls.Add(OpenFlpBtn);
            Controls.Add(ProgressBar);
            Name = "Form1";
            Text = "Choisir un fichier";
            ResumeLayout(false);
        }

        #endregion

        private Button FileBtn;
        private Button StemBtn;
        private Button MidiBtn;
        private Button PresetBtn;
        private Button CreateFlpBtn;
        private Button OpenFlpBtn;
        private ProgressBar ProgressBar;
    }
}
