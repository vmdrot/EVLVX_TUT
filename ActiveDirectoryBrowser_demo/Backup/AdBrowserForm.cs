using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.DirectoryServices;

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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Computers",
																													 "container"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))));
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Ou1",
																													 "organizationalUnit",
																													 "my first organizational unit"}, 1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))));
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "Users",
																													 "container"}, 2, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0))));
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdBrowserForm));
			this.treeView_ad = new System.Windows.Forms.TreeView();
			this.splitter_mainForm = new System.Windows.Forms.Splitter();
			this.listView_ad = new System.Windows.Forms.ListView();
			this.imageList_adObjects = new System.Windows.Forms.ImageList(this.components);
			this.columnHeader_name = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_type = new System.Windows.Forms.ColumnHeader();
			this.columnHeader_description = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// treeView_ad
			// 
			this.treeView_ad.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView_ad.ImageList = this.imageList_adObjects;
			this.treeView_ad.Name = "treeView_ad";
			this.treeView_ad.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					new System.Windows.Forms.TreeNode("dc=test,dc=com", 0, 0, new System.Windows.Forms.TreeNode[] {
																																													  new System.Windows.Forms.TreeNode("Computers", 2, 3),
																																													  new System.Windows.Forms.TreeNode("Users", 2, 3, new System.Windows.Forms.TreeNode[] {
																																																																			   new System.Windows.Forms.TreeNode("Node6", 5, 5),
																																																																			   new System.Windows.Forms.TreeNode("Node7", 5, 5)}),
																																													  new System.Windows.Forms.TreeNode("Ou1", 1, 3, new System.Windows.Forms.TreeNode[] {
																																																																			 new System.Windows.Forms.TreeNode("ExampleComputer", 4, 4),
																																																																			 new System.Windows.Forms.TreeNode("Example Group", 6, 6),
																																																																			 new System.Windows.Forms.TreeNode("Unknown object", 7, 7)})})});
			this.treeView_ad.Size = new System.Drawing.Size(192, 338);
			this.treeView_ad.Sorted = true;
			this.treeView_ad.TabIndex = 0;
			this.treeView_ad.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_ad_AfterSelect);
			// 
			// splitter_mainForm
			// 
			this.splitter_mainForm.Location = new System.Drawing.Point(192, 0);
			this.splitter_mainForm.Name = "splitter_mainForm";
			this.splitter_mainForm.Size = new System.Drawing.Size(3, 338);
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
			this.listView_ad.Location = new System.Drawing.Point(195, 0);
			this.listView_ad.Name = "listView_ad";
			this.listView_ad.Size = new System.Drawing.Size(405, 338);
			this.listView_ad.SmallImageList = this.imageList_adObjects;
			this.listView_ad.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView_ad.TabIndex = 2;
			this.listView_ad.View = System.Windows.Forms.View.Details;
			this.listView_ad.DoubleClick += new System.EventHandler(this.listView_ad_DoubleClick);
			this.listView_ad.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ad_ColumnClick);
			// 
			// imageList_adObjects
			// 
			this.imageList_adObjects.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList_adObjects.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList_adObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_adObjects.ImageStream")));
			this.imageList_adObjects.TransparentColor = System.Drawing.Color.Transparent;
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
			// AdBrowserForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 338);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView_ad,
																		  this.splitter_mainForm,
																		  this.treeView_ad});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(608, 365);
			this.Name = "AdBrowserForm";
			this.Text = "Simple Active Directory Browser";
			this.ResumeLayout(false);

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
					default:
						MessageBox.Show("You double-clicked another object");
						break;
				}
			}
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
	}
}
