namespace Evolvex.Ruthenorum.JIRAAuth.Tests.Forms
{
    partial class MiscTestParamsForm
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
            this.lblJson = new System.Windows.Forms.Label();
            this.edJson = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblJson
            // 
            this.lblJson.AutoSize = true;
            this.lblJson.Location = new System.Drawing.Point(13, 13);
            this.lblJson.Name = "lblJson";
            this.lblJson.Size = new System.Drawing.Size(38, 13);
            this.lblJson.TabIndex = 0;
            this.lblJson.Text = "JSON:";
            // 
            // edJson
            // 
            this.edJson.Location = new System.Drawing.Point(55, 13);
            this.edJson.Multiline = true;
            this.edJson.Name = "edJson";
            this.edJson.Size = new System.Drawing.Size(393, 181);
            this.edJson.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(368, 200);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // MiscTestParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 236);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.edJson);
            this.Controls.Add(this.lblJson);
            this.Name = "MiscTestParamsForm";
            this.Text = "MiscTestParamsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJson;
        private System.Windows.Forms.TextBox edJson;
        private System.Windows.Forms.Button btnOK;
    }
}