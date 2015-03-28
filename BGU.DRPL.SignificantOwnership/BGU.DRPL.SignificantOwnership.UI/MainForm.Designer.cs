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
            this.appx2LPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericPersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.appx2LPToolStripMenuItem,
            this.appx2LPproperToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.newToolStripMenuItem.Text = "New...";
            // 
            // appx2LPToolStripMenuItem
            // 
            this.appx2LPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acqToolStripMenuItem,
            this.genericPersonToolStripMenuItem,
            this.addressToolStripMenuItem,
            this.countryToolStripMenuItem});
            this.appx2LPToolStripMenuItem.Name = "appx2LPToolStripMenuItem";
            this.appx2LPToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.appx2LPToolStripMenuItem.Text = "Appx2LP";
            this.appx2LPToolStripMenuItem.Click += new System.EventHandler(this.appx2LPToolStripMenuItem_Click);
            // 
            // acqToolStripMenuItem
            // 
            this.acqToolStripMenuItem.Name = "acqToolStripMenuItem";
            this.acqToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.acqToolStripMenuItem.Text = "Acquiree";
            this.acqToolStripMenuItem.Click += new System.EventHandler(this.acqToolStripMenuItem_Click);
            // 
            // genericPersonToolStripMenuItem
            // 
            this.genericPersonToolStripMenuItem.Name = "genericPersonToolStripMenuItem";
            this.genericPersonToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.genericPersonToolStripMenuItem.Text = "GenericPerson";
            this.genericPersonToolStripMenuItem.Click += new System.EventHandler(this.genericPersonToolStripMenuItem_Click);
            // 
            // addressToolStripMenuItem
            // 
            this.addressToolStripMenuItem.Name = "addressToolStripMenuItem";
            this.addressToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addressToolStripMenuItem.Text = "Address";
            this.addressToolStripMenuItem.Click += new System.EventHandler(this.addressToolStripMenuItem_Click);
            // 
            // countryToolStripMenuItem
            // 
            this.countryToolStripMenuItem.Name = "countryToolStripMenuItem";
            this.countryToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.countryToolStripMenuItem.Text = "Country";
            this.countryToolStripMenuItem.Click += new System.EventHandler(this.countryToolStripMenuItem_Click);
            // 
            // appx2LPproperToolStripMenuItem
            // 
            this.appx2LPproperToolStripMenuItem.Name = "appx2LPproperToolStripMenuItem";
            this.appx2LPproperToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.appx2LPproperToolStripMenuItem.Text = "Appx2LP (proper)";
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
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appx2LPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericPersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appx2LPproperToolStripMenuItem;
    }
}

