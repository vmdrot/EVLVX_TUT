namespace SimpleAdBrowser
{
    partial class Connect2ADDlg
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
            this.edADDomain = new System.Windows.Forms.TextBox();
            this.edADRoot = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edUsr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "AD domain:";
            // 
            // edADDomain
            // 
            this.edADDomain.Location = new System.Drawing.Point(87, 13);
            this.edADDomain.Name = "edADDomain";
            this.edADDomain.Size = new System.Drawing.Size(360, 20);
            this.edADDomain.TabIndex = 1;
            // 
            // edADRoot
            // 
            this.edADRoot.Location = new System.Drawing.Point(87, 37);
            this.edADRoot.Name = "edADRoot";
            this.edADRoot.Size = new System.Drawing.Size(360, 20);
            this.edADRoot.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Root:";
            // 
            // edUsr
            // 
            this.edUsr.Location = new System.Drawing.Point(87, 62);
            this.edUsr.Name = "edUsr";
            this.edUsr.Size = new System.Drawing.Size(220, 20);
            this.edUsr.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "User:";
            // 
            // edPwd
            // 
            this.edPwd.Location = new System.Drawing.Point(87, 88);
            this.edPwd.Name = "edPwd";
            this.edPwd.PasswordChar = '*';
            this.edPwd.Size = new System.Drawing.Size(220, 20);
            this.edPwd.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(315, 77);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 77);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Connect2ADDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(483, 118);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.edPwd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.edUsr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edADRoot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edADDomain);
            this.Controls.Add(this.label1);
            this.Name = "Connect2ADDlg";
            this.Text = "Connect2ADDlg";
            this.Load += new System.EventHandler(this.Connect2ADDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edADDomain;
        private System.Windows.Forms.TextBox edADRoot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edUsr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}