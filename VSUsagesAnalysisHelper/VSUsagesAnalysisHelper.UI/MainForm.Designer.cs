namespace VSUsagesAnalysisHelper.UI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtRaw = new System.Windows.Forms.TextBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgvwMissingFiles = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtTrimmedJSONCnt = new System.Windows.Forms.TextBox();
            this.txtTrimmedJSON = new System.Windows.Forms.TextBox();
            this.txtJsonTempCnt = new System.Windows.Forms.TextBox();
            this.txtJsonTemp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edDefFile = new System.Windows.Forms.TextBox();
            this.edDefType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIsEnd = new System.Windows.Forms.CheckBox();
            this.edRecordAs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAppend = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCopyFileMethodFromGrid = new System.Windows.Forms.Button();
            this.edSkipClasses = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnViewTree = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwMissingFiles)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "VS K+R:";
            // 
            // txtRaw
            // 
            this.txtRaw.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRaw.Location = new System.Drawing.Point(69, 40);
            this.txtRaw.Multiline = true;
            this.txtRaw.Name = "txtRaw";
            this.txtRaw.Size = new System.Drawing.Size(1227, 140);
            this.txtRaw.TabIndex = 7;
            // 
            // btnParse
            // 
            this.btnParse.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnParse.Location = new System.Drawing.Point(4, 157);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(49, 23);
            this.btnParse.TabIndex = 9;
            this.btnParse.Text = "Pa&rse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 242);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 475);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bindingNavigator1);
            this.tabPage1.Controls.Add(this.dgvwMissingFiles);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1276, 446);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tab 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.BindingSource = this.bindingSource1;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(3, 3);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1270, 31);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 28);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 28);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // dgvwMissingFiles
            // 
            this.dgvwMissingFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvwMissingFiles.AutoGenerateColumns = false;
            this.dgvwMissingFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvwMissingFiles.DataSource = this.bindingSource1;
            this.dgvwMissingFiles.Location = new System.Drawing.Point(3, 47);
            this.dgvwMissingFiles.Name = "dgvwMissingFiles";
            this.dgvwMissingFiles.RowHeadersWidth = 51;
            this.dgvwMissingFiles.RowTemplate.Height = 24;
            this.dgvwMissingFiles.Size = new System.Drawing.Size(1267, 400);
            this.dgvwMissingFiles.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtTrimmedJSONCnt);
            this.tabPage2.Controls.Add(this.txtTrimmedJSON);
            this.tabPage2.Controls.Add(this.txtJsonTempCnt);
            this.tabPage2.Controls.Add(this.txtJsonTemp);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1276, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tab 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtTrimmedJSONCnt
            // 
            this.txtTrimmedJSONCnt.Location = new System.Drawing.Point(9, 132);
            this.txtTrimmedJSONCnt.Name = "txtTrimmedJSONCnt";
            this.txtTrimmedJSONCnt.Size = new System.Drawing.Size(100, 22);
            this.txtTrimmedJSONCnt.TabIndex = 10;
            // 
            // txtTrimmedJSON
            // 
            this.txtTrimmedJSON.Location = new System.Drawing.Point(9, 89);
            this.txtTrimmedJSON.Multiline = true;
            this.txtTrimmedJSON.Name = "txtTrimmedJSON";
            this.txtTrimmedJSON.Size = new System.Drawing.Size(1258, 37);
            this.txtTrimmedJSON.TabIndex = 9;
            // 
            // txtJsonTempCnt
            // 
            this.txtJsonTempCnt.Location = new System.Drawing.Point(9, 61);
            this.txtJsonTempCnt.Name = "txtJsonTempCnt";
            this.txtJsonTempCnt.Size = new System.Drawing.Size(100, 22);
            this.txtJsonTempCnt.TabIndex = 8;
            // 
            // txtJsonTemp
            // 
            this.txtJsonTemp.Location = new System.Drawing.Point(9, 8);
            this.txtJsonTemp.Multiline = true;
            this.txtJsonTemp.Name = "txtJsonTemp";
            this.txtJsonTemp.Size = new System.Drawing.Size(1258, 47);
            this.txtJsonTemp.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "File";
            // 
            // edDefFile
            // 
            this.edDefFile.Location = new System.Drawing.Point(40, 10);
            this.edDefFile.Name = "edDefFile";
            this.edDefFile.Size = new System.Drawing.Size(795, 22);
            this.edDefFile.TabIndex = 1;
            this.edDefFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // edDefType
            // 
            this.edDefType.Location = new System.Drawing.Point(877, 8);
            this.edDefType.Name = "edDefType";
            this.edDefType.Size = new System.Drawing.Size(349, 22);
            this.edDefType.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(841, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Type";
            // 
            // chkIsEnd
            // 
            this.chkIsEnd.AutoSize = true;
            this.chkIsEnd.Location = new System.Drawing.Point(1233, 8);
            this.chkIsEnd.Name = "chkIsEnd";
            this.chkIsEnd.Size = new System.Drawing.Size(68, 21);
            this.chkIsEnd.TabIndex = 5;
            this.chkIsEnd.Text = "Is en&d";
            this.chkIsEnd.UseVisualStyleBackColor = true;
            // 
            // edRecordAs
            // 
            this.edRecordAs.AcceptsReturn = true;
            this.edRecordAs.Location = new System.Drawing.Point(81, 186);
            this.edRecordAs.Name = "edRecordAs";
            this.edRecordAs.Size = new System.Drawing.Size(958, 22);
            this.edRecordAs.TabIndex = 11;
            this.edRecordAs.Text = "D:\\home\\vmdrot\\Work\\EPAM\\Projs\\VRTF-IMGR\\JIRA\\US55812\\BlobStorage_usages.json";
            this.edRecordAs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Record As:";
            // 
            // chkAppend
            // 
            this.chkAppend.AutoSize = true;
            this.chkAppend.Checked = true;
            this.chkAppend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAppend.Location = new System.Drawing.Point(1045, 186);
            this.chkAppend.Name = "chkAppend";
            this.chkAppend.Size = new System.Drawing.Size(79, 21);
            this.chkAppend.TabIndex = 16;
            this.chkAppend.Text = "&Append";
            this.chkAppend.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1176, 183);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(113, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Sa&ve";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1118, 183);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(52, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Re&fresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCopyFileMethodFromGrid
            // 
            this.btnCopyFileMethodFromGrid.Location = new System.Drawing.Point(12, 214);
            this.btnCopyFileMethodFromGrid.Name = "btnCopyFileMethodFromGrid";
            this.btnCopyFileMethodFromGrid.Size = new System.Drawing.Size(75, 23);
            this.btnCopyFileMethodFromGrid.TabIndex = 19;
            this.btnCopyFileMethodFromGrid.Text = "Cop&y";
            this.btnCopyFileMethodFromGrid.UseVisualStyleBackColor = true;
            this.btnCopyFileMethodFromGrid.Click += new System.EventHandler(this.btnCopyFileMethodFromGrid_Click);
            // 
            // edSkipClasses
            // 
            this.edSkipClasses.AcceptsReturn = true;
            this.edSkipClasses.Location = new System.Drawing.Point(199, 215);
            this.edSkipClasses.Name = "edSkipClasses";
            this.edSkipClasses.Size = new System.Drawing.Size(958, 22);
            this.edSkipClasses.TabIndex = 20;
            this.edSkipClasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.edSkipClasses.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Skip classes:";
            // 
            // btnViewTree
            // 
            this.btnViewTree.Location = new System.Drawing.Point(1164, 218);
            this.btnViewTree.Name = "btnViewTree";
            this.btnViewTree.Size = new System.Drawing.Size(103, 23);
            this.btnViewTree.TabIndex = 22;
            this.btnViewTree.Text = "View Tree";
            this.btnViewTree.UseVisualStyleBackColor = true;
            this.btnViewTree.Click += new System.EventHandler(this.btnViewTree_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1308, 726);
            this.Controls.Add(this.btnViewTree);
            this.Controls.Add(this.edSkipClasses);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCopyFileMethodFromGrid);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkAppend);
            this.Controls.Add(this.edRecordAs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkIsEnd);
            this.Controls.Add(this.edDefType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.edDefFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnParse);
            this.Controls.Add(this.txtRaw);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Usages analyzer";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvwMissingFiles)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRaw;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtTrimmedJSONCnt;
        private System.Windows.Forms.TextBox txtTrimmedJSON;
        private System.Windows.Forms.TextBox txtJsonTempCnt;
        private System.Windows.Forms.TextBox txtJsonTemp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edDefFile;
        private System.Windows.Forms.TextBox edDefType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIsEnd;
        private System.Windows.Forms.TextBox edRecordAs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAppend;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvwMissingFiles;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCopyFileMethodFromGrid;
        private System.Windows.Forms.TextBox edSkipClasses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnViewTree;
    }
}

