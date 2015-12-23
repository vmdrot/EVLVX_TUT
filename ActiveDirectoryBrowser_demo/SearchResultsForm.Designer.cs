namespace SimpleAdBrowser
{
    partial class SearchResultsForm
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
            this.edEmail = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.dgvw = new System.Windows.Forms.DataGridView();
            this.edSearchADRoot = new System.Windows.Forms.TextBox();
            this.edQuery = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvw)).BeginInit();
            this.SuspendLayout();
            // 
            // edEmail
            // 
            this.edEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edEmail.Location = new System.Drawing.Point(445, 11);
            this.edEmail.Name = "edEmail";
            this.edEmail.Size = new System.Drawing.Size(297, 20);
            this.edEmail.TabIndex = 3;
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Location = new System.Drawing.Point(752, 11);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(83, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "&Find / Зн&айти";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // dgvw
            // 
            this.dgvw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvw.Location = new System.Drawing.Point(13, 68);
            this.dgvw.Name = "dgvw";
            this.dgvw.Size = new System.Drawing.Size(822, 361);
            this.dgvw.TabIndex = 9;
            this.dgvw.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvw_CellFormatting);
            this.dgvw.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvw_CellMouseDoubleClick);
            this.dgvw.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvw_RowPostPaint);
            this.dgvw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvw_KeyUp);
            // 
            // edSearchADRoot
            // 
            this.edSearchADRoot.Location = new System.Drawing.Point(57, 11);
            this.edSearchADRoot.Name = "edSearchADRoot";
            this.edSearchADRoot.Size = new System.Drawing.Size(368, 20);
            this.edSearchADRoot.TabIndex = 1;
            // 
            // edQuery
            // 
            this.edQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edQuery.Location = new System.Drawing.Point(57, 40);
            this.edQuery.Name = "edQuery";
            this.edQuery.Size = new System.Drawing.Size(685, 20);
            this.edQuery.TabIndex = 5;
            // 
            // SearchResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 441);
            this.Controls.Add(this.edQuery);
            this.Controls.Add(this.edSearchADRoot);
            this.Controls.Add(this.dgvw);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.edEmail);
            this.Name = "SearchResultsForm";
            this.Text = "SearchResultsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edEmail;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DataGridView dgvw;
        private System.Windows.Forms.TextBox edSearchADRoot;
        private System.Windows.Forms.TextBox edQuery;
    }
}