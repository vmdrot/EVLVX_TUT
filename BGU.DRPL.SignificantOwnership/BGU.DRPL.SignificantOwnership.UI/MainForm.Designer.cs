namespace BGU.DRPL.SignificantOwnership.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regLicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regLicAppx7ShareAcqIntentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regLicAppx14NewSvcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regLicAppx4PhysPQuestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ownershipStructToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appx2LPproperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(401, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regLicToolStripMenuItem,
            this.ownershipStructToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // regLicToolStripMenuItem
            // 
            this.regLicToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regLicAppx7ShareAcqIntentToolStripMenuItem,
            this.regLicAppx14NewSvcToolStripMenuItem,
            this.regLicAppx4PhysPQuestToolStripMenuItem});
            this.regLicToolStripMenuItem.Name = "regLicToolStripMenuItem";
            this.regLicToolStripMenuItem.Size = new System.Drawing.Size(442, 22);
            this.regLicToolStripMenuItem.Text = "Реєстрація та ліцензування банків, відкриття відокремлених підрозділів";
            // 
            // regLicAppx7ShareAcqIntentToolStripMenuItem
            // 
            this.regLicAppx7ShareAcqIntentToolStripMenuItem.Name = "regLicAppx7ShareAcqIntentToolStripMenuItem";
            this.regLicAppx7ShareAcqIntentToolStripMenuItem.Size = new System.Drawing.Size(678, 22);
            this.regLicAppx7ShareAcqIntentToolStripMenuItem.Text = "ПОВІДОМЛЕННЯ про наміри набуття/збільшення істотної участі в банку (Додаток 7)";
            this.regLicAppx7ShareAcqIntentToolStripMenuItem.Click += new System.EventHandler(this.regLicAppx7ShareAcqIntentToolStripMenuItem_Click);
            // 
            // regLicAppx14NewSvcToolStripMenuItem
            // 
            this.regLicAppx14NewSvcToolStripMenuItem.Name = "regLicAppx14NewSvcToolStripMenuItem";
            this.regLicAppx14NewSvcToolStripMenuItem.Size = new System.Drawing.Size(678, 22);
            this.regLicAppx14NewSvcToolStripMenuItem.Text = "ПОВІДОМЛЕННЯ банку про початок нового виду діяльності або надання нового виду фін" +
    "ансових послуг (Додаток 14)";
            this.regLicAppx14NewSvcToolStripMenuItem.Click += new System.EventHandler(this.regLicAppx14NewSvcToolStripMenuItem_Click);
            // 
            // regLicAppx4PhysPQuestToolStripMenuItem
            // 
            this.regLicAppx4PhysPQuestToolStripMenuItem.Name = "regLicAppx4PhysPQuestToolStripMenuItem";
            this.regLicAppx4PhysPQuestToolStripMenuItem.Size = new System.Drawing.Size(678, 22);
            this.regLicAppx4PhysPQuestToolStripMenuItem.Text = "АНКЕТА фізичної особи (Додаток 4)";
            this.regLicAppx4PhysPQuestToolStripMenuItem.Click += new System.EventHandler(this.regLicAppx4PhysPQuestToolStripMenuItem_Click);
            // 
            // ownershipStructToolStripMenuItem
            // 
            this.ownershipStructToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appx2LPproperToolStripMenuItem});
            this.ownershipStructToolStripMenuItem.Name = "ownershipStructToolStripMenuItem";
            this.ownershipStructToolStripMenuItem.Size = new System.Drawing.Size(442, 22);
            this.ownershipStructToolStripMenuItem.Text = "Відомості про структуру власності";
            // 
            // appx2LPproperToolStripMenuItem
            // 
            this.appx2LPproperToolStripMenuItem.Name = "appx2LPproperToolStripMenuItem";
            this.appx2LPproperToolStripMenuItem.Size = new System.Drawing.Size(373, 22);
            this.appx2LPproperToolStripMenuItem.Text = "АНКЕТА юридичної особи (у тому числі банку) (Додаток 2)";
            this.appx2LPproperToolStripMenuItem.Click += new System.EventHandler(this.appx2LPproperToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 273);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "NBU: Significant ownership (basic UI)";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appx2LPproperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regLicAppx7ShareAcqIntentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regLicAppx14NewSvcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regLicAppx4PhysPQuestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regLicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ownershipStructToolStripMenuItem;
    }
}

