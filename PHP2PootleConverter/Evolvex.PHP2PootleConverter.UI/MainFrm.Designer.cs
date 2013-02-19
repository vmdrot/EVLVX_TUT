namespace Evolvex.PHP2PootleConverter.UI
{
    partial class MainFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.edSourceDir = new System.Windows.Forms.TextBox();
            this.gbTargetDir = new System.Windows.Forms.GroupBox();
            this.chkTargetDirSameAsSource = new System.Windows.Forms.CheckBox();
            this.lblTargetDir = new System.Windows.Forms.Label();
            this.edTargetDir = new System.Windows.Forms.TextBox();
            this.gbDirection = new System.Windows.Forms.GroupBox();
            this.rbPo2PHP = new System.Windows.Forms.RadioButton();
            this.rbPHP2Po = new System.Windows.Forms.RadioButton();
            this.edSourceLanguageName = new System.Windows.Forms.TextBox();
            this.lblSourceLanguageName = new System.Windows.Forms.Label();
            this.edTargetLanguageName = new System.Windows.Forms.TextBox();
            this.lblTargetLanguageName = new System.Windows.Forms.Label();
            this.chkDeleteSourceFiles = new System.Windows.Forms.CheckBox();
            this.lblSaveAsEncoding = new System.Windows.Forms.Label();
            this.cbxSaveAsEncoding = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbTargetDir.SuspendLayout();
            this.gbDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SourceDir";
            // 
            // edSourceDir
            // 
            this.edSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edSourceDir.Location = new System.Drawing.Point(108, 13);
            this.edSourceDir.Name = "edSourceDir";
            this.edSourceDir.Size = new System.Drawing.Size(410, 20);
            this.edSourceDir.TabIndex = 1;
            // 
            // gbTargetDir
            // 
            this.gbTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTargetDir.Controls.Add(this.chkTargetDirSameAsSource);
            this.gbTargetDir.Controls.Add(this.lblTargetDir);
            this.gbTargetDir.Controls.Add(this.edTargetDir);
            this.gbTargetDir.Location = new System.Drawing.Point(3, 44);
            this.gbTargetDir.Name = "gbTargetDir";
            this.gbTargetDir.Size = new System.Drawing.Size(609, 73);
            this.gbTargetDir.TabIndex = 4;
            this.gbTargetDir.TabStop = false;
            this.gbTargetDir.Text = "TargetDir";
            // 
            // chkTargetDirSameAsSource
            // 
            this.chkTargetDirSameAsSource.AutoSize = true;
            this.chkTargetDirSameAsSource.Location = new System.Drawing.Point(105, 16);
            this.chkTargetDirSameAsSource.Name = "chkTargetDirSameAsSource";
            this.chkTargetDirSameAsSource.Size = new System.Drawing.Size(118, 17);
            this.chkTargetDirSameAsSource.TabIndex = 7;
            this.chkTargetDirSameAsSource.Text = "same as the source";
            this.chkTargetDirSameAsSource.UseVisualStyleBackColor = true;
            this.chkTargetDirSameAsSource.CheckedChanged += new System.EventHandler(this.chkTargetDirSameAsSource_CheckedChanged);
            // 
            // lblTargetDir
            // 
            this.lblTargetDir.AutoSize = true;
            this.lblTargetDir.Location = new System.Drawing.Point(10, 42);
            this.lblTargetDir.Name = "lblTargetDir";
            this.lblTargetDir.Size = new System.Drawing.Size(43, 13);
            this.lblTargetDir.TabIndex = 6;
            this.lblTargetDir.Text = "another";
            // 
            // edTargetDir
            // 
            this.edTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edTargetDir.Location = new System.Drawing.Point(105, 39);
            this.edTargetDir.Name = "edTargetDir";
            this.edTargetDir.Size = new System.Drawing.Size(410, 20);
            this.edTargetDir.TabIndex = 5;
            // 
            // gbDirection
            // 
            this.gbDirection.Controls.Add(this.rbPo2PHP);
            this.gbDirection.Controls.Add(this.rbPHP2Po);
            this.gbDirection.Location = new System.Drawing.Point(3, 124);
            this.gbDirection.Name = "gbDirection";
            this.gbDirection.Size = new System.Drawing.Size(211, 43);
            this.gbDirection.TabIndex = 5;
            this.gbDirection.TabStop = false;
            this.gbDirection.Text = "Direction";
            // 
            // rbPo2PHP
            // 
            this.rbPo2PHP.AutoSize = true;
            this.rbPo2PHP.Location = new System.Drawing.Point(116, 20);
            this.rbPo2PHP.Name = "rbPo2PHP";
            this.rbPo2PHP.Size = new System.Drawing.Size(89, 17);
            this.rbPo2PHP.TabIndex = 1;
            this.rbPo2PHP.TabStop = true;
            this.rbPo2PHP.Text = "Pootle-2-PHP";
            this.rbPo2PHP.UseVisualStyleBackColor = true;
            // 
            // rbPHP2Po
            // 
            this.rbPHP2Po.AutoSize = true;
            this.rbPHP2Po.Location = new System.Drawing.Point(10, 20);
            this.rbPHP2Po.Name = "rbPHP2Po";
            this.rbPHP2Po.Size = new System.Drawing.Size(89, 17);
            this.rbPHP2Po.TabIndex = 0;
            this.rbPHP2Po.TabStop = true;
            this.rbPHP2Po.Text = "PHP-2-Pootle";
            this.rbPHP2Po.UseVisualStyleBackColor = true;
            // 
            // edSourceLanguageName
            // 
            this.edSourceLanguageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edSourceLanguageName.Location = new System.Drawing.Point(108, 173);
            this.edSourceLanguageName.Name = "edSourceLanguageName";
            this.edSourceLanguageName.Size = new System.Drawing.Size(410, 20);
            this.edSourceLanguageName.TabIndex = 7;
            // 
            // lblSourceLanguageName
            // 
            this.lblSourceLanguageName.AutoSize = true;
            this.lblSourceLanguageName.Location = new System.Drawing.Point(10, 176);
            this.lblSourceLanguageName.Name = "lblSourceLanguageName";
            this.lblSourceLanguageName.Size = new System.Drawing.Size(89, 13);
            this.lblSourceLanguageName.TabIndex = 6;
            this.lblSourceLanguageName.Text = "SourceLanguage";
            // 
            // edTargetLanguageName
            // 
            this.edTargetLanguageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.edTargetLanguageName.Location = new System.Drawing.Point(108, 211);
            this.edTargetLanguageName.Name = "edTargetLanguageName";
            this.edTargetLanguageName.Size = new System.Drawing.Size(410, 20);
            this.edTargetLanguageName.TabIndex = 9;
            // 
            // lblTargetLanguageName
            // 
            this.lblTargetLanguageName.AutoSize = true;
            this.lblTargetLanguageName.Location = new System.Drawing.Point(10, 211);
            this.lblTargetLanguageName.Name = "lblTargetLanguageName";
            this.lblTargetLanguageName.Size = new System.Drawing.Size(86, 13);
            this.lblTargetLanguageName.TabIndex = 8;
            this.lblTargetLanguageName.Text = "TargetLanguage";
            // 
            // chkDeleteSourceFiles
            // 
            this.chkDeleteSourceFiles.AutoSize = true;
            this.chkDeleteSourceFiles.Location = new System.Drawing.Point(108, 237);
            this.chkDeleteSourceFiles.Name = "chkDeleteSourceFiles";
            this.chkDeleteSourceFiles.Size = new System.Drawing.Size(112, 17);
            this.chkDeleteSourceFiles.TabIndex = 10;
            this.chkDeleteSourceFiles.Text = "DeleteSourceFiles";
            this.chkDeleteSourceFiles.UseVisualStyleBackColor = true;
            // 
            // lblSaveAsEncoding
            // 
            this.lblSaveAsEncoding.AutoSize = true;
            this.lblSaveAsEncoding.Location = new System.Drawing.Point(10, 267);
            this.lblSaveAsEncoding.Name = "lblSaveAsEncoding";
            this.lblSaveAsEncoding.Size = new System.Drawing.Size(89, 13);
            this.lblSaveAsEncoding.TabIndex = 11;
            this.lblSaveAsEncoding.Text = "SaveAsEncoding";
            // 
            // cbxSaveAsEncoding
            // 
            this.cbxSaveAsEncoding.FormattingEnabled = true;
            this.cbxSaveAsEncoding.Location = new System.Drawing.Point(108, 264);
            this.cbxSaveAsEncoding.Name = "cbxSaveAsEncoding";
            this.cbxSaveAsEncoding.Size = new System.Drawing.Size(197, 21);
            this.cbxSaveAsEncoding.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(429, 309);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(510, 309);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MainFrm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 344);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxSaveAsEncoding);
            this.Controls.Add(this.lblSaveAsEncoding);
            this.Controls.Add(this.chkDeleteSourceFiles);
            this.Controls.Add(this.edTargetLanguageName);
            this.Controls.Add(this.lblTargetLanguageName);
            this.Controls.Add(this.edSourceLanguageName);
            this.Controls.Add(this.lblSourceLanguageName);
            this.Controls.Add(this.gbDirection);
            this.Controls.Add(this.gbTargetDir);
            this.Controls.Add(this.edSourceDir);
            this.Controls.Add(this.label1);
            this.Name = "MainFrm";
            this.Text = "PHP-2-Pootle converter";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.gbTargetDir.ResumeLayout(false);
            this.gbTargetDir.PerformLayout();
            this.gbDirection.ResumeLayout(false);
            this.gbDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edSourceDir;
        private System.Windows.Forms.GroupBox gbTargetDir;
        private System.Windows.Forms.TextBox edTargetDir;
        private System.Windows.Forms.CheckBox chkTargetDirSameAsSource;
        private System.Windows.Forms.Label lblTargetDir;
        private System.Windows.Forms.GroupBox gbDirection;
        private System.Windows.Forms.RadioButton rbPo2PHP;
        private System.Windows.Forms.RadioButton rbPHP2Po;
        private System.Windows.Forms.TextBox edSourceLanguageName;
        private System.Windows.Forms.Label lblSourceLanguageName;
        private System.Windows.Forms.TextBox edTargetLanguageName;
        private System.Windows.Forms.Label lblTargetLanguageName;
        private System.Windows.Forms.CheckBox chkDeleteSourceFiles;
        private System.Windows.Forms.Label lblSaveAsEncoding;
        private System.Windows.Forms.ComboBox cbxSaveAsEncoding;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}

