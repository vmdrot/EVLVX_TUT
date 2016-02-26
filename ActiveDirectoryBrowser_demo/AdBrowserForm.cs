using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.DirectoryServices;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace SimpleAdBrowser
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AdBrowserForm : System.Windows.Forms.Form
	{
		private enum AdImages
		{
			AdRoot,
			Ou,
			Container,
			OpenContainer,
			Computer,
			User,
			Group,
			Unknown,
			Unavailable
		}

		private DirectoryEntry _AdRootDSE = null;
		private DirectoryEntry _AdRoot = null;

		// column sorter for the listview
		private ListViewColumnSorter lvwColumnSorter;

		private System.Windows.Forms.TreeView treeView_ad;
		private System.Windows.Forms.ListView listView_ad;
		private System.Windows.Forms.ImageList imageList_adObjects;
		private System.Windows.Forms.Splitter splitter_mainForm;
		private System.Windows.Forms.ColumnHeader columnHeader_name;
		private System.Windows.Forms.ColumnHeader columnHeader_type;
		private System.Windows.Forms.ColumnHeader columnHeader_description;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem userToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem usersByListToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem allUsersToolStripMenuItem;
		private System.ComponentModel.IContainer components;

		public AdBrowserForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			myInitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Computers", 2, 3);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Example Group", 6, 6);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ExampleComputer", 4, 4);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Unknown object", 7, 7);
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Ou1", 1, 3, new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node6", 5, 5);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Node7", 5, 5);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Users", 2, 3, new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("dc=test,dc=com", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5,
            treeNode8});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdBrowserForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Computers",
            "container"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ou1",
            "organizationalUnit",
            "my first organizational unit"}, 1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "Users",
            "container"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            this.treeView_ad = new System.Windows.Forms.TreeView();
            this.imageList_adObjects = new System.Windows.Forms.ImageList(this.components);
            this.splitter_mainForm = new System.Windows.Forms.Splitter();
            this.listView_ad = new System.Windows.Forms.ListView();
            this.columnHeader_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersByListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.allUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_ad
            // 
            this.treeView_ad.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView_ad.ImageIndex = 0;
            this.treeView_ad.ImageList = this.imageList_adObjects;
            this.treeView_ad.Location = new System.Drawing.Point(0, 24);
            this.treeView_ad.Name = "treeView_ad";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "";
            treeNode1.SelectedImageIndex = 3;
            treeNode1.Text = "Computers";
            treeNode2.ImageIndex = 6;
            treeNode2.Name = "";
            treeNode2.SelectedImageIndex = 6;
            treeNode2.Text = "Example Group";
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "";
            treeNode3.SelectedImageIndex = 4;
            treeNode3.Text = "ExampleComputer";
            treeNode4.ImageIndex = 7;
            treeNode4.Name = "";
            treeNode4.SelectedImageIndex = 7;
            treeNode4.Text = "Unknown object";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "";
            treeNode5.SelectedImageIndex = 3;
            treeNode5.Text = "Ou1";
            treeNode6.ImageIndex = 5;
            treeNode6.Name = "";
            treeNode6.SelectedImageIndex = 5;
            treeNode6.Text = "Node6";
            treeNode7.ImageIndex = 5;
            treeNode7.Name = "";
            treeNode7.SelectedImageIndex = 5;
            treeNode7.Text = "Node7";
            treeNode8.ImageIndex = 2;
            treeNode8.Name = "";
            treeNode8.SelectedImageIndex = 3;
            treeNode8.Text = "Users";
            treeNode9.ImageIndex = 0;
            treeNode9.Name = "";
            treeNode9.SelectedImageIndex = 0;
            treeNode9.Text = "dc=test,dc=com";
            this.treeView_ad.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9});
            this.treeView_ad.SelectedImageIndex = 0;
            this.treeView_ad.Size = new System.Drawing.Size(192, 314);
            this.treeView_ad.Sorted = true;
            this.treeView_ad.TabIndex = 0;
            this.treeView_ad.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_ad_AfterSelect);
            // 
            // imageList_adObjects
            // 
            this.imageList_adObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_adObjects.ImageStream")));
            this.imageList_adObjects.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_adObjects.Images.SetKeyName(0, "");
            this.imageList_adObjects.Images.SetKeyName(1, "");
            this.imageList_adObjects.Images.SetKeyName(2, "");
            this.imageList_adObjects.Images.SetKeyName(3, "");
            this.imageList_adObjects.Images.SetKeyName(4, "");
            this.imageList_adObjects.Images.SetKeyName(5, "");
            this.imageList_adObjects.Images.SetKeyName(6, "");
            this.imageList_adObjects.Images.SetKeyName(7, "");
            this.imageList_adObjects.Images.SetKeyName(8, "");
            // 
            // splitter_mainForm
            // 
            this.splitter_mainForm.Location = new System.Drawing.Point(192, 24);
            this.splitter_mainForm.Name = "splitter_mainForm";
            this.splitter_mainForm.Size = new System.Drawing.Size(3, 314);
            this.splitter_mainForm.TabIndex = 1;
            this.splitter_mainForm.TabStop = false;
            // 
            // listView_ad
            // 
            this.listView_ad.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_name,
            this.columnHeader_type,
            this.columnHeader_description});
            this.listView_ad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_ad.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView_ad.Location = new System.Drawing.Point(195, 24);
            this.listView_ad.Name = "listView_ad";
            this.listView_ad.Size = new System.Drawing.Size(405, 314);
            this.listView_ad.SmallImageList = this.imageList_adObjects;
            this.listView_ad.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_ad.TabIndex = 2;
            this.listView_ad.UseCompatibleStateImageBehavior = false;
            this.listView_ad.View = System.Windows.Forms.View.Details;
            this.listView_ad.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ad_ColumnClick);
            this.listView_ad.DoubleClick += new System.EventHandler(this.listView_ad_DoubleClick);
            // 
            // columnHeader_name
            // 
            this.columnHeader_name.Text = "Name";
            this.columnHeader_name.Width = 150;
            // 
            // columnHeader_type
            // 
            this.columnHeader_type.Text = "Type";
            this.columnHeader_type.Width = 100;
            // 
            // columnHeader_description
            // 
            this.columnHeader_description.Text = "Description";
            this.columnHeader_description.Width = 150;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.userToolStripMenuItem.Text = "User";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usersByListToolStripMenuItem,
            this.allUsersToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.exportToolStripMenuItem.Text = "Export...";
            // 
            // usersByListToolStripMenuItem
            // 
            this.usersByListToolStripMenuItem.Name = "usersByListToolStripMenuItem";
            this.usersByListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.usersByListToolStripMenuItem.Text = "Users By List";
            this.usersByListToolStripMenuItem.Click += new System.EventHandler(this.usersByListToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // allUsersToolStripMenuItem
            // 
            this.allUsersToolStripMenuItem.Name = "allUsersToolStripMenuItem";
            this.allUsersToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.allUsersToolStripMenuItem.Text = "All Users";
            this.allUsersToolStripMenuItem.Click += new System.EventHandler(this.allUsersToolStripMenuItem_Click);
            // 
            // AdBrowserForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 338);
            this.Controls.Add(this.listView_ad);
            this.Controls.Add(this.splitter_mainForm);
            this.Controls.Add(this.treeView_ad);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(608, 365);
            this.Name = "AdBrowserForm";
            this.Text = "Simple Active Directory Browser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new AdBrowserForm());
			}
			catch (Exception exc)
			{
				MessageBox.Show("Error occured in application. Need to close. Error was: " + exc.Message);
			}
		}

		private void myInitializeComponent()
		{
			lvwColumnSorter = new ListViewColumnSorter();
			this.listView_ad.ListViewItemSorter = lvwColumnSorter;

			try
			{
				this._AdRootDSE = new DirectoryEntry("LDAP://rootDSE");
				this._AdRoot = new DirectoryEntry("LDAP://" + (string)this._AdRootDSE.Properties["defaultNamingContext"].Value);
				/*
				foreach(string property in this._AdRoot.Properties.PropertyNames)
				{
					MessageBox.Show(property + " = " + this._AdRoot.Properties[property].Value);
				}*/

				TreeNode root = new TreeNode((string)this._AdRootDSE.Properties["defaultNamingContext"].Value,(int)AdImages.AdRoot,(int)AdImages.AdRoot);
				
				root.Tag = this._AdRoot;
				this.treeView_ad.Nodes.Clear();
				this.treeView_ad.Nodes.Add(root);
			}
			catch
			{
				throw new Exception("Error connecting to AD");
			}
		}

		private void treeView_ad_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeView myTreeView = (TreeView)sender;
			DirectoryEntry selectedEntry = (DirectoryEntry)e.Node.Tag;

			if (!DirectoryEntry.Exists(selectedEntry.Path))
			{
				e.Node.ImageIndex = (int)AdImages.Unavailable;
				e.Node.SelectedImageIndex = (int)AdImages.Unavailable;
				
				return;
			}

			// cleanup the current node 
			this.listView_ad.Items.Clear();
			e.Node.Nodes.Clear();

			myTreeView.BeginUpdate();

			try
			{
				foreach (DirectoryEntry child in selectedEntry.Children)
				{			
					TreeNode tmpNode = null;
					ListViewItem tmpItem = null;

					switch(child.SchemaClassName)
					{
						case "organizationalUnit":
							tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.Ou,(int)AdImages.OpenContainer);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.Ou);
							break;
						case "container":
							tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.Container,(int)AdImages.OpenContainer);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.Container);
							break;
						case "computer":
							//tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.Computer,(int)AdImages.Computer);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.Computer);
							break;
						case "user":
							//tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.User,(int)AdImages.User);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.User);
							break;
						case "group":
							//tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.Group,(int)AdImages.Group);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.Group);
							break;
						default:
							//tmpNode = new TreeNode((string)child.Properties["name"].Value,(int)AdImages.Unknown,(int)AdImages.Unknown);
							tmpItem = new ListViewItem(new string[] {
																		(string)child.Properties["name"].Value,
																		child.SchemaClassName,
																		(string)child.Properties["description"].Value

																	},(int)AdImages.Unknown);
							break;
					}

					// save the directory entry reference in the tag
					if (tmpNode!=null) 
					{ 
						tmpNode.Tag = child; 
						e.Node.Nodes.Add(tmpNode);
					}
					if (tmpItem!=null) 
					{ 
						tmpItem.Tag = child; 
						this.listView_ad.Items.Add(tmpItem);
					}					
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
				// active directory exception ???
			}

			e.Node.Expand();

			myTreeView.EndUpdate();
		}

		private void listView_ad_DoubleClick(object sender, System.EventArgs e)
		{
			ListView myListView = (ListView)sender;

			if (myListView.SelectedItems.Count == 1)
			{
				ListViewItem myListViewItem = myListView.SelectedItems[0];
				DirectoryEntry myObject = (DirectoryEntry)myListViewItem.Tag;
				if (myObject == null)
					return;

				switch (myListViewItem.ImageIndex)
				{
					case (int) AdImages.Ou:
					case (int) AdImages.Container:
						foreach (TreeNode tNode in this.treeView_ad.SelectedNode.Nodes)
						{
							if (tNode.Text.Equals(myListViewItem.Text))
							{
								this.treeView_ad.SelectedNode = tNode;
								if (myListView.Items.Count >0)
								{
									myListView.Focus();
									myListView.Items[0].Selected = true;										
								}
								break;
							}
						}
						break;	
					case (int) AdImages.User:
                        ShowUserPropertiesForm(TreeFullPath2DomainRootPath(treeView_ad.SelectedNode.FullPath), myListView.SelectedItems[0].Text);
                        break;
                    case (int)AdImages.Computer:
                        ShowComputerPropertiesForm(TreeFullPath2DomainRootPath(treeView_ad.SelectedNode.FullPath), myListView.SelectedItems[0].Text);
                        break;
                    default:
						MessageBox.Show("You double-clicked another object");
						break;
				}
			}
		}

        private string TreeFullPath2DomainPath(string treePath)
        {
            string[] words = treePath.Split('\\');
            //StringBuilder rslt = new StringBuilder("LDAP://");
            StringBuilder rslt = new StringBuilder();
            for (int i = words.Length - 1; i > 0; i--)
            {
                rslt.AppendFormat("DC={0},", words[i]);
            }
            rslt.Append(words[0]);
            //rslt.Append('/');
            return rslt.ToString();
        }
        private string TreeFullPath2DomainRootPath(string treePath)
        {
            string[] words = treePath.Split('\\');
            StringBuilder rslt = new StringBuilder("LDAP://");
            rslt.Append(words[0]);
            //rslt.Append('/');
            return rslt.ToString();
        }

        public static void ShowUserPropertiesForm(string itemPath, string usr)
        {
            ShowUserPropertiesForm(SearchResultsForm.GetADUser(itemPath, usr));
        }


        public static void ShowComputerPropertiesForm(string itemPath, string usr)
        {
            DirectoryEntry de = new DirectoryEntry(itemPath.Replace("LDAP://", "LDAP://CN=" + usr + ","));
            //ShowUserPropertiesForm(SearchResultsForm.GetADUser(itemPath, usr));
        }


        public static Dictionary<string, object> ReadUserProps(SearchResult rs)
        {
            Dictionary<string, object> rslt = new Dictionary<string, object>();

            foreach (DictionaryEntry prop in rs.Properties)
            {
                List<object> currProps = new List<object>();
                object val = prop.Value;
                object currPropVal = null;
                if (prop.Value != null)
                {
                    if (prop.Value is System.DirectoryServices.ResultPropertyValueCollection)
                    {
                        System.DirectoryServices.ResultPropertyValueCollection propVals = (System.DirectoryServices.ResultPropertyValueCollection)prop.Value;
                        List<object> currPropValLst = new List<object>();
                        if (propVals.Count > 0)
                        {
                            for (int i = 0; i < propVals.Count; i++)
                            {
                                val = propVals[i];
                                currPropValLst.Add(val);
                            }
                            currPropVal = (object)currPropValLst;
                        }
                    }
                    else
                    {
                        currPropVal = prop.Value;
                    }
                }
                rslt.Add(prop.Key as string, currPropVal);
            }

            return rslt;
        }

        public static void ShowUserPropertiesForm(SearchResult rs)
        {
            //DirectoryEntry de = new DirectoryEntry(string.Format("LDAP://CN={0},{1}", usr, itemPath));
            StringBuilder sb = new StringBuilder();

            //rs.Properties.PropertyNames;
            //rs.Properties.Values.
            //for(int i = 0; i<res.Properties.Count; i++)
            //{ 
            //    res.Properties
            //}

            foreach (DictionaryEntry prop in rs.Properties)
            {
                StringBuilder currSb = new StringBuilder();
                object val = prop.Value;
                if (prop.Value != null && prop.Value is System.DirectoryServices.ResultPropertyValueCollection)
                {
                    System.DirectoryServices.ResultPropertyValueCollection propVals = (System.DirectoryServices.ResultPropertyValueCollection)prop.Value;
                    if (propVals.Count > 0)
                    {
                        for (int i = 0; i < propVals.Count; i++)
                        {
                            val = propVals[i];
                            if (i > 0)
                                currSb.Append("\n\r");
                            currSb.Append(val);
                        }
                        val = currSb.ToString();
                    }
                }
                sb.AppendLine(string.Format("{0}: {1}", prop.Key, val));
            }

            MessageBox.Show(sb.ToString());
        }

		private void listView_ad_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			ListView myListView = (ListView)sender;

			// Determine if clicked column is already the column that is being sorted.
			if ( e.Column == lvwColumnSorter.SortColumn )
			{
				// Reverse the current sort direction for this column.
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			myListView.Sort();
		}

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchResultsForm frm = new SearchResultsForm();
            frm.RootDSE = _AdRoot.Path;
            frm.ShowDialog();
        }

        private void usersByListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if(saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            //string[] lines = File.ReadAllLines(openFileDialog1.FileName);
            Dictionary<string, List<string>> lines = ReadNamesLinesWithPossibleTabDelimitedExtraFields(File.ReadAllLines(openFileDialog1.FileName));
            StringBuilder sb = new StringBuilder();
            List<List<SearchResult>> rslts = new List<List<SearchResult>>();
            foreach (string line in lines.Keys)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                string trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed))
                    continue;
                List<SearchResult> currRslt = SearchResultsForm.GetADUsers(_AdRoot.Path, string.Empty, string.Format("(&(objectClass=user)(objectCategory=person)(displayname={0}))", trimmed));
                if (currRslt == null || currRslt.Count == 0)
                    continue;
                rslts.Add(currRslt);
                string dispName = trimmed;
                string email = ReadStringPropSafe(currRslt[0], "mail");
                string userId = ReadStringPropSafe(currRslt[0], "samaccountname");
                string phoneNr = "(" + ReadStringPropSafe(currRslt[0], "telephonenumber").Replace(' ', ')');
                string position = ReadStringPropSafe(currRslt[0], "title");
                string tabNr = ReadStringPropSafe(currRslt[0], "employeeid");
                string addr = ReadStringPropSafe(currRslt[0], "physicaldeliveryofficename");
                string dept = ReadStringPropSafe(currRslt[0], "department");
                string compName = FindUserComputer(tabNr, line);

                //sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}{8}", dispName, tabNr, userId, email, phoneNr, position, addr, compName, FormatOtherFields(lines[line])));

                //№ з/п	Прізвище, ім’я,   по батькові користувача
	
                //Ідентифікатор користувача (наданий службою захисту)	Повна назва посади корис-тувача
                //    Назва підрозділу, в якому працює користувач
                //    Поштова адреса  (вул., буд., корп., кімн.)
                //користувача
                //    Телефон міський, внутрішній
                //корис-тувача
                //    Мережеве ім’я
                //корис-тувача
                //    Повноваження
                //користувача
                // (перегляд/ внесення інформації)


                sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}{8}", dispName, userId, position, dept, addr, phoneNr, compName, email, FormatOtherFields(lines[line])));
            }

            //File.WriteAllText(JsonConvert.SerializeObject(rslts, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), @"D:\home\vmdrot\DEV\ActiveDirectoryBrowser_demo\last_searchres.json", Encoding.Unicode);
            WriteXML<List<List<SearchResult>>>(rslts, @"D:\home\vmdrot\DEV\ActiveDirectoryBrowser_demo\last_searchres.xml");
            File.WriteAllText(saveFileDialog1.FileName, sb.ToString(), Encoding.Unicode);
        }

        public static void WriteXML<T>(T obj, string saveAs)
        {
            using (FileStream fs = File.Create(saveAs))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fs, obj);
            }
        }

        private string FindUserComputer(string tabNr, string dispNm)
        {
            List<SearchResult> findings = SearchResultsForm.GetADComputerByUser(_AdRoot.Path, tabNr, dispNm);
            if (findings == null || findings.Count == 0)
                return string.Empty;
            return ReadStringPropSafe(findings[0], "name");
        }

        private string FormatOtherFields(List<string> list)
        {
            if (list == null || list.Count == 0)
                return string.Empty;
            return ("\t" + String.Join("\t", list.ToArray()));
        }

        private Dictionary<string, List<string>> ReadNamesLinesWithPossibleTabDelimitedExtraFields(string[] rawLines)
        {
            Dictionary<string, List<string>> rslt = new Dictionary<string, List<string>>();
            foreach (string rawLine in rawLines)
            {
                if (string.IsNullOrEmpty(rawLine))
                    continue;
                string[] fields = rawLine.Split('\t');
                string currName = fields[0];
                List<string> currOtherFields = new List<string>();
                if (fields.Length > 1)
                {
                    for (int i = 1; i < fields.Length; i++)
                        currOtherFields.Add(fields[i]);
                }
                rslt.Add(currName, currOtherFields);
            }
            return rslt;
        }

        private string ReadStringPropSafe(SearchResult searchResult, string propName)
        {
            if(searchResult == null)
            return string.Empty;
            if(searchResult.Properties[propName] == null || searchResult.Properties[propName].Count == 0)
                return string.Empty;
            return searchResult.Properties[propName][0] as string;
        }

        private void allUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // ReadUserProps

            List<Dictionary<string, object>> usrs = new List<Dictionary<string,object>>();
            Dictionary<string, int> stats = new Dictionary<string, int>();
            string allCyrLetters = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЧЦШЩЮЯ";
            List<string> surnameStarts = new List<string>();
            foreach(char ch in allCyrLetters)
            {
                foreach(char ch2 in allCyrLetters)
                {
                    string currStart = string.Format("{0}{1}", ch, string.Format("{0}", ch2).ToLower());
                    surnameStarts.Add(currStart);
                }
            }

            foreach(string ch in surnameStarts)
            {
                List<SearchResult> currRslt = SearchResultsForm.GetADUsers(_AdRoot.Path, string.Empty, string.Format("(&(objectClass=user)(objectCategory=person)(displayName={0}*))", ch));
                stats.Add(ch, currRslt.Count);
                foreach(SearchResult sr in currRslt)
                {
                    usrs.Add(ReadUserProps(sr));
                }
            }
            //WriteXML<List<Dictionary<string, object>>>(usrs, @"D:\home\vmdrot\DEV\ActiveDirectoryBrowser_demo\all_users_search_res.xml");
            File.WriteAllText(@"D:\home\vmdrot\DEV\ActiveDirectoryBrowser_demo\all_users_search_stats.json", JsonConvert.SerializeObject(stats, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
            File.WriteAllText(@"D:\home\vmdrot\DEV\ActiveDirectoryBrowser_demo\all_users_search_res.json", JsonConvert.SerializeObject(usrs, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode);
        }
	}
}
