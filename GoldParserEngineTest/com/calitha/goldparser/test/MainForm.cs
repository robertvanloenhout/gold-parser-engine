using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;
using com.calitha.goldparser;
using com.calitha.commons;

namespace com.calitha.goldparser.test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class GoldForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage sourcePage;
		private System.Windows.Forms.TabPage parseActionsPage;
		private System.Windows.Forms.TabPage parseTreePage;
		private System.Windows.Forms.RichTextBox sourceTextBox;
		private System.Windows.Forms.TreeView parseTreeView;
		private System.Windows.Forms.ListView parseActionsView;
		private System.Windows.Forms.ColumnHeader actionHeader;
		private System.Windows.Forms.RichTextBox logBox;
		private System.Windows.Forms.Panel sourcePanel;
		private System.Windows.Forms.Panel parseActionsPanel;
		private System.Windows.Forms.Panel parseTreePanel;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.MenuItem fileItem;
		private System.Windows.Forms.MenuItem exitItem;
		private System.Windows.Forms.MenuItem helpItem;
		private System.Windows.Forms.MenuItem aboutItem;
		private System.Windows.Forms.MenuItem openGrammarItem;
		private System.Windows.Forms.ToolBarButton openGrammarButton;
		private System.Windows.Forms.ToolBarButton logButton;
		private System.Windows.Forms.ToolBarButton exitButton;
		private System.Windows.Forms.ToolBarButton testGrammarButton;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.OpenFileDialog openGrammarDialog;
		private System.Windows.Forms.Button parseButton;
		private System.Windows.Forms.ImageList treeImageList;
		private System.Windows.Forms.ColumnHeader descriptionHeader;
		private System.Windows.Forms.ColumnHeader valueHeader;
		private System.Windows.Forms.ColumnHeader stateHeader;
		private System.Windows.Forms.ColumnHeader positionHeader;
		private System.Windows.Forms.ColumnHeader lineHeader;
		private System.Windows.Forms.ColumnHeader columnHeader;
		private System.Windows.Forms.ImageList viewImageList;
		private System.Windows.Forms.CheckBox trimReductionsBox;
		private System.Windows.Forms.ComboBox maxErrorsBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuItem setFontMenuItem;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.Button openSourceButton;
		private System.Windows.Forms.OpenFileDialog openSourceDialog;
		private System.ComponentModel.IContainer components;

		public GoldForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			string cfgFile = GetConfigFilename();
			try
			{
				settings = UserSettings.Load(cfgFile);
				WriteLn("Settings have been loaded");
			}
			catch (Exception)
			{
				WriteLn("Unable to load settings. Default settings will be used instead.");
				settings = new UserSettings();
				SaveSettings();
			}
			SetFontInViews();
			maxErrorsBox.SelectedIndex = settings.MaxErrorsIndex;
			openSourceDialog.FileName = settings.LastSource;
			openGrammarDialog.FileName = settings.LastGrammar;
			trimReductionsBox.Checked = settings.TrimReductions;

			WriteLn("Tip: Set a Unicode font in the view menu if you want to see Unicode characters");
		}

		/// <summary>
		/// Gets the configuration filename for this application.
		/// </summary>
		/// <returns></returns>
		public string GetConfigFilename()
		{
			return Path.ChangeExtension(Application.ExecutablePath, ".cfg");
		}

		/// <summary>
		/// Tries to save the user settings. Ignores any errors.
		/// </summary>
		public void SaveSettings()
		{
			try
			{
				settings.Save(GetConfigFilename());
			}
			catch (Exception)
			{}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GoldForm));
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.openGrammarButton = new System.Windows.Forms.ToolBarButton();
			this.testGrammarButton = new System.Windows.Forms.ToolBarButton();
			this.logButton = new System.Windows.Forms.ToolBarButton();
			this.exitButton = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.logBox = new System.Windows.Forms.RichTextBox();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.sourcePage = new System.Windows.Forms.TabPage();
			this.sourcePanel = new System.Windows.Forms.Panel();
			this.openSourceButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.maxErrorsBox = new System.Windows.Forms.ComboBox();
			this.trimReductionsBox = new System.Windows.Forms.CheckBox();
			this.parseButton = new System.Windows.Forms.Button();
			this.sourceTextBox = new System.Windows.Forms.RichTextBox();
			this.parseActionsPage = new System.Windows.Forms.TabPage();
			this.parseActionsPanel = new System.Windows.Forms.Panel();
			this.parseActionsView = new System.Windows.Forms.ListView();
			this.actionHeader = new System.Windows.Forms.ColumnHeader();
			this.positionHeader = new System.Windows.Forms.ColumnHeader();
			this.lineHeader = new System.Windows.Forms.ColumnHeader();
			this.columnHeader = new System.Windows.Forms.ColumnHeader();
			this.descriptionHeader = new System.Windows.Forms.ColumnHeader();
			this.valueHeader = new System.Windows.Forms.ColumnHeader();
			this.stateHeader = new System.Windows.Forms.ColumnHeader();
			this.viewImageList = new System.Windows.Forms.ImageList(this.components);
			this.parseTreePage = new System.Windows.Forms.TabPage();
			this.parseTreePanel = new System.Windows.Forms.Panel();
			this.parseTreeView = new System.Windows.Forms.TreeView();
			this.treeImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainPanel = new System.Windows.Forms.Panel();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.fileItem = new System.Windows.Forms.MenuItem();
			this.openGrammarItem = new System.Windows.Forms.MenuItem();
			this.exitItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.setFontMenuItem = new System.Windows.Forms.MenuItem();
			this.helpItem = new System.Windows.Forms.MenuItem();
			this.aboutItem = new System.Windows.Forms.MenuItem();
			this.openGrammarDialog = new System.Windows.Forms.OpenFileDialog();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.openSourceDialog = new System.Windows.Forms.OpenFileDialog();
			this.tabControl.SuspendLayout();
			this.sourcePage.SuspendLayout();
			this.sourcePanel.SuspendLayout();
			this.parseActionsPage.SuspendLayout();
			this.parseActionsPanel.SuspendLayout();
			this.parseTreePage.SuspendLayout();
			this.parseTreePanel.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.openGrammarButton,
																					   this.testGrammarButton,
																					   this.logButton,
																					   this.exitButton});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(784, 44);
			this.toolBar.TabIndex = 6;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// openGrammarButton
			// 
			this.openGrammarButton.ImageIndex = 0;
			this.openGrammarButton.ToolTipText = "Open Grammar File";
			// 
			// testGrammarButton
			// 
			this.testGrammarButton.ImageIndex = 5;
			this.testGrammarButton.ToolTipText = "Test Grammar";
			// 
			// logButton
			// 
			this.logButton.ImageIndex = 1;
			this.logButton.ToolTipText = "View Log";
			// 
			// exitButton
			// 
			this.exitButton.ImageIndex = 3;
			this.exitButton.ToolTipText = "Exit";
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 507);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(784, 22);
			this.statusBar.TabIndex = 7;
			// 
			// logBox
			// 
			this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.logBox.Location = new System.Drawing.Point(5, 5);
			this.logBox.Name = "logBox";
			this.logBox.ReadOnly = true;
			this.logBox.Size = new System.Drawing.Size(774, 453);
			this.logBox.TabIndex = 8;
			this.logBox.Text = "";
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.sourcePage);
			this.tabControl.Controls.Add(this.parseActionsPage);
			this.tabControl.Controls.Add(this.parseTreePage);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(5, 5);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(774, 453);
			this.tabControl.TabIndex = 10;
			this.tabControl.Visible = false;
			// 
			// sourcePage
			// 
			this.sourcePage.Controls.Add(this.sourcePanel);
			this.sourcePage.Location = new System.Drawing.Point(4, 22);
			this.sourcePage.Name = "sourcePage";
			this.sourcePage.Size = new System.Drawing.Size(766, 427);
			this.sourcePage.TabIndex = 0;
			this.sourcePage.Text = "Source";
			// 
			// sourcePanel
			// 
			this.sourcePanel.Controls.Add(this.openSourceButton);
			this.sourcePanel.Controls.Add(this.label1);
			this.sourcePanel.Controls.Add(this.maxErrorsBox);
			this.sourcePanel.Controls.Add(this.trimReductionsBox);
			this.sourcePanel.Controls.Add(this.parseButton);
			this.sourcePanel.Controls.Add(this.sourceTextBox);
			this.sourcePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourcePanel.DockPadding.Bottom = 40;
			this.sourcePanel.DockPadding.Left = 5;
			this.sourcePanel.DockPadding.Right = 5;
			this.sourcePanel.DockPadding.Top = 5;
			this.sourcePanel.Location = new System.Drawing.Point(0, 0);
			this.sourcePanel.Name = "sourcePanel";
			this.sourcePanel.Size = new System.Drawing.Size(766, 427);
			this.sourcePanel.TabIndex = 1;
			// 
			// openSourceButton
			// 
			this.openSourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.openSourceButton.Location = new System.Drawing.Point(8, 400);
			this.openSourceButton.Name = "openSourceButton";
			this.openSourceButton.TabIndex = 5;
			this.openSourceButton.Text = "Source...";
			this.openSourceButton.Click += new System.EventHandler(this.openSourceButton_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(296, 400);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "Maximum errors:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maxErrorsBox
			// 
			this.maxErrorsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.maxErrorsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.maxErrorsBox.Items.AddRange(new object[] {
															  "0",
															  "1",
															  "2",
															  "3",
															  "4",
															  "5",
															  "6",
															  "7",
															  "8",
															  "9",
															  "10",
															  "100",
															  "1000"});
			this.maxErrorsBox.Location = new System.Drawing.Point(408, 400);
			this.maxErrorsBox.Name = "maxErrorsBox";
			this.maxErrorsBox.Size = new System.Drawing.Size(88, 21);
			this.maxErrorsBox.TabIndex = 3;
			this.maxErrorsBox.SelectedIndexChanged += new System.EventHandler(this.maxErrorsBox_SelectedIndexChanged);
			// 
			// trimReductionsBox
			// 
			this.trimReductionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.trimReductionsBox.Location = new System.Drawing.Point(184, 400);
			this.trimReductionsBox.Name = "trimReductionsBox";
			this.trimReductionsBox.TabIndex = 2;
			this.trimReductionsBox.Text = "Trim reductions";
			this.trimReductionsBox.CheckedChanged += new System.EventHandler(this.trimReductionsBox_CheckedChanged);
			// 
			// parseButton
			// 
			this.parseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.parseButton.Location = new System.Drawing.Point(96, 400);
			this.parseButton.Name = "parseButton";
			this.parseButton.TabIndex = 1;
			this.parseButton.Text = "Parse";
			this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
			// 
			// sourceTextBox
			// 
			this.sourceTextBox.AcceptsTab = true;
			this.sourceTextBox.DetectUrls = false;
			this.sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourceTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.sourceTextBox.Location = new System.Drawing.Point(5, 5);
			this.sourceTextBox.Name = "sourceTextBox";
			this.sourceTextBox.Size = new System.Drawing.Size(756, 382);
			this.sourceTextBox.TabIndex = 0;
			this.sourceTextBox.Text = "";
			// 
			// parseActionsPage
			// 
			this.parseActionsPage.Controls.Add(this.parseActionsPanel);
			this.parseActionsPage.Location = new System.Drawing.Point(4, 22);
			this.parseActionsPage.Name = "parseActionsPage";
			this.parseActionsPage.Size = new System.Drawing.Size(766, 429);
			this.parseActionsPage.TabIndex = 1;
			this.parseActionsPage.Text = "Parse Actions";
			// 
			// parseActionsPanel
			// 
			this.parseActionsPanel.Controls.Add(this.parseActionsView);
			this.parseActionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.parseActionsPanel.DockPadding.All = 5;
			this.parseActionsPanel.Location = new System.Drawing.Point(0, 0);
			this.parseActionsPanel.Name = "parseActionsPanel";
			this.parseActionsPanel.Size = new System.Drawing.Size(766, 429);
			this.parseActionsPanel.TabIndex = 1;
			// 
			// parseActionsView
			// 
			this.parseActionsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.actionHeader,
																							   this.positionHeader,
																							   this.lineHeader,
																							   this.columnHeader,
																							   this.descriptionHeader,
																							   this.valueHeader,
																							   this.stateHeader});
			this.parseActionsView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.parseActionsView.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.parseActionsView.FullRowSelect = true;
			this.parseActionsView.GridLines = true;
			this.parseActionsView.Location = new System.Drawing.Point(5, 5);
			this.parseActionsView.Name = "parseActionsView";
			this.parseActionsView.Size = new System.Drawing.Size(756, 419);
			this.parseActionsView.SmallImageList = this.viewImageList;
			this.parseActionsView.TabIndex = 0;
			this.parseActionsView.View = System.Windows.Forms.View.Details;
			// 
			// actionHeader
			// 
			this.actionHeader.Text = "Action";
			this.actionHeader.Width = 110;
			// 
			// positionHeader
			// 
			this.positionHeader.Text = "pos";
			this.positionHeader.Width = 62;
			// 
			// lineHeader
			// 
			this.lineHeader.Text = "ln";
			this.lineHeader.Width = 40;
			// 
			// columnHeader
			// 
			this.columnHeader.Text = "col";
			this.columnHeader.Width = 38;
			// 
			// descriptionHeader
			// 
			this.descriptionHeader.Text = "Description";
			this.descriptionHeader.Width = 285;
			// 
			// valueHeader
			// 
			this.valueHeader.Text = "Value";
			this.valueHeader.Width = 145;
			// 
			// stateHeader
			// 
			this.stateHeader.Text = "State";
			this.stateHeader.Width = 81;
			// 
			// viewImageList
			// 
			this.viewImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.viewImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.viewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("viewImageList.ImageStream")));
			this.viewImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// parseTreePage
			// 
			this.parseTreePage.Controls.Add(this.parseTreePanel);
			this.parseTreePage.Location = new System.Drawing.Point(4, 22);
			this.parseTreePage.Name = "parseTreePage";
			this.parseTreePage.Size = new System.Drawing.Size(766, 429);
			this.parseTreePage.TabIndex = 2;
			this.parseTreePage.Text = "Parse Tree";
			// 
			// parseTreePanel
			// 
			this.parseTreePanel.Controls.Add(this.parseTreeView);
			this.parseTreePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.parseTreePanel.DockPadding.All = 5;
			this.parseTreePanel.Location = new System.Drawing.Point(0, 0);
			this.parseTreePanel.Name = "parseTreePanel";
			this.parseTreePanel.Size = new System.Drawing.Size(766, 429);
			this.parseTreePanel.TabIndex = 1;
			// 
			// parseTreeView
			// 
			this.parseTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.parseTreeView.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.parseTreeView.ImageList = this.treeImageList;
			this.parseTreeView.Location = new System.Drawing.Point(5, 5);
			this.parseTreeView.Name = "parseTreeView";
			this.parseTreeView.Size = new System.Drawing.Size(756, 419);
			this.parseTreeView.TabIndex = 0;
			// 
			// treeImageList
			// 
			this.treeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.treeImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
			this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.tabControl);
			this.mainPanel.Controls.Add(this.logBox);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.DockPadding.All = 5;
			this.mainPanel.Location = new System.Drawing.Point(0, 44);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(784, 463);
			this.mainPanel.TabIndex = 11;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.fileItem,
																					 this.menuItem1,
																					 this.helpItem});
			// 
			// fileItem
			// 
			this.fileItem.Index = 0;
			this.fileItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.openGrammarItem,
																					 this.exitItem});
			this.fileItem.Text = "File";
			// 
			// openGrammarItem
			// 
			this.openGrammarItem.Index = 0;
			this.openGrammarItem.Text = "&Open Grammar";
			this.openGrammarItem.Click += new System.EventHandler(this.openGrammarClick);
			// 
			// exitItem
			// 
			this.exitItem.Index = 1;
			this.exitItem.Text = "Exit";
			this.exitItem.Click += new System.EventHandler(this.exitClick);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.setFontMenuItem});
			this.menuItem1.Text = "View";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Test Grammar";
			this.menuItem2.Click += new System.EventHandler(this.testGrammarClick);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "View Log";
			this.menuItem3.Click += new System.EventHandler(this.viewLogClick);
			// 
			// setFontMenuItem
			// 
			this.setFontMenuItem.Index = 2;
			this.setFontMenuItem.Text = "Set Font";
			this.setFontMenuItem.Click += new System.EventHandler(this.setFontMenuItem_Click);
			// 
			// helpItem
			// 
			this.helpItem.Index = 2;
			this.helpItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.aboutItem});
			this.helpItem.Text = "Help";
			// 
			// aboutItem
			// 
			this.aboutItem.Index = 0;
			this.aboutItem.Text = "About";
			this.aboutItem.Click += new System.EventHandler(this.aboutClick);
			// 
			// openGrammarDialog
			// 
			this.openGrammarDialog.Filter = "Compiled Grammar Table files (*.cgt)|*.cgt|All Files (*.*)|*.*";
			// 
			// fontDialog
			// 
			this.fontDialog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			// 
			// openSourceDialog
			// 
			this.openSourceDialog.Filter = "All Files (*.*)|*.*";
			// 
			// GoldForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(784, 529);
			this.Controls.Add(this.mainPanel);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.toolBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "GoldForm";
			this.Text = "GOLD Parser Engine Test";
			this.tabControl.ResumeLayout(false);
			this.sourcePage.ResumeLayout(false);
			this.sourcePanel.ResumeLayout(false);
			this.parseActionsPage.ResumeLayout(false);
			this.parseActionsPanel.ResumeLayout(false);
			this.parseTreePage.ResumeLayout(false);
			this.parseTreePanel.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new GoldForm());
		}

		UserSettings settings;
		LALRParser parser;
		int maxerrors = 0;
		int errors = 0;
		private byte[] BOM = new byte[] {0xFF,0xFE};


		/// <summary>
		/// Writes a log line to the user interface.
		/// </summary>
		/// <param name="str"></param>
		public void WriteLn(string str)
		{
			logBox.AppendText(str);
			logBox.AppendText("\n");
		}
		
		/// <summary>
		/// Event handler for the action of reading a token.
		/// </summary>
		/// <param name="parser">parser that is the source of this event</param>
		/// <param name="args">event arguments</param>
		private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
		{
			AddViewItem("Token Read", 
				args.Token.Location,
				args.Token.Symbol.ToString(),
				StringUtil.ShowEscapeChars(args.Token.Text),"",0);
		}

		/// <summary>
		/// Event handler for the shift action.
		/// </summary>
		/// <param name="parser">parser that is the source of this event</param>
		/// <param name="args">event arguments</param>
		private void ShiftEvent(LALRParser parser, ShiftEventArgs args)
		{
			AddViewItem("Shift",
				args.Token.Location,
				args.Token.Symbol.ToString(),
				StringUtil.ShowEscapeChars(args.Token.Text),
				args.NewState.Id.ToString(),
				1);
		}

		/// <summary>
		/// Event handler for the reduce action.
		/// </summary>
		/// <param name="parser">parser that is the source of this event</param>
		/// <param name="args">event arguments</param>
		private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
		{
			//WriteLn(args.Rule.Id.ToString());
			//WriteLn(args.Rule.Lhs.Id.ToString());
			AddViewItem("Reduce",
				null,
				args.Token.Rule.ToString(),
                "",
				args.NewState.Id.ToString(),
				2);
		}

		/// <summary>
		/// Event handlers for the goto action.
		/// </summary>
		/// <param name="parser">parser that is the source of this event</param>
		/// <param name="args">parser that is the source of this event</param>
		public void GotoEvent(LALRParser parser, GotoEventArgs args)
		{
			AddViewItem("Goto",
				null,
				args.Symbol.ToString(),
                "",
				args.NewState.Id.ToString(),
				3);
		}

		private void FillTreeViewRecursive(TreeView tree, TreeNode parentNode, Token token)
		{
			if (token is TerminalToken)
			{
				TerminalToken t = (TerminalToken)token;
				TreeNode node = new TreeNode(StringUtil.ShowEscapeChars(t.Text),0,0);
				parentNode.Nodes.Add(node);
			}
			else if (token is NonterminalToken)
			{
				NonterminalToken t = (NonterminalToken)token;
				TreeNode node = new TreeNode(t.Rule.ToString(),1,1);
				parentNode.Nodes.Add(node);
				foreach (Token childToken in t.Tokens)
				{
					FillTreeViewRecursive(tree,node,childToken);
				}
			}
		}

		private void FillTreeView(TreeView tree, NonterminalToken token)
		{
			TreeNode node = new TreeNode(token.Rule.ToString(),1,1);
			tree.Nodes.Add(node);
			foreach (Token childToken in token.Tokens)
			{
				FillTreeViewRecursive(tree,node,childToken);
			}
		}

		private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
		{
			AddViewItem("Accept",null,"","","",4);
			FillTreeView(parseTreeView,args.Token);
			tabControl.SelectedIndex = 2;
		}

		private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
		{
			AddViewItem("Token error", args.Token.Location, "Cannot recognize token",
				args.Token.Text,"",5);
			errors++;
			if (errors <= maxerrors)
				args.Continue = true;
		}

		private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
		{
			AddViewItem("Parse error", null, "Expecting the following tokens:",
				args.ExpectedTokens.ToString(),"",5);
			errors++;
			
			if (errors <= maxerrors)
			{
				args.Continue = ContinueMode.Skip;
				// example for inserting a new token
				/*
				args.Continue = ContinueMode.Insert;
				TerminalToken token = new TerminalToken((SymbolTerminal)parser.Symbols.Get(4),
				                                        "555",
				                                        new Location(0, 0, 0));
				args.NextToken = token;
				*/
			}
		}

		/// <summary>
		/// Event handler for the reading a comment.
		/// </summary>
		/// <param name="parser">parser that is the source of this event</param>
		/// <param name="args">event arguments</param>
		private void CommentReadEvent(LALRParser parser, CommentReadEventArgs args)
		{
		    string desc;
		    if (args.LineComment)
		        desc = "Line Comment";
		    else
		        desc = "Block Comment";
			AddViewItem("Comment Read", null, desc, args.Content, "", 6);
		}

		private void AddViewItem(String action, Location location, String description,
			String value, String state, int type)
		{
			string[] strItems = new string[7];
			strItems[0] = action;
			if (location != null)
			{
				strItems[1] = location.Position.ToString();
				strItems[2] = (location.LineNr+1).ToString();
				strItems[3] = (location.ColumnNr+1).ToString();
			}
			strItems[4] = description;
			strItems[5] = StringUtil.ShowEscapeChars(value);
			strItems[6] = state;
			ListViewItem item = new ListViewItem(strItems,type);
			switch (type)
			{
				case 0: item.ForeColor = Color.Gray; break;
				case 1: break;
				case 2: break;
				case 3: break;
				case 4: item.ForeColor = Color.Green; break;
				case 5: item.ForeColor = Color.Red; break;
				case 6: item.ForeColor = Color.Gray; break;
			}
			parseActionsView.Items.Add(item);
		}

		private void ActivateLog()
		{
			logBox.Visible = true;
			tabControl.Hide();
		}

		private void ActivateTest()
		{
			tabControl.Visible = true;
			logBox.Hide();
		}

		private void OpenGrammar()
		{
            if (openGrammarDialog.ShowDialog() == DialogResult.OK)
			{
				settings.LastGrammar = openGrammarDialog.FileName;
				SaveSettings();
				WriteLn("Reading file...");
				long t1 = DateTime.Now.Ticks;
				try
				{
					CGTReader reader = new CGTReader(openGrammarDialog.FileName);
					parser = reader.CreateNewParser();
					parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
					parser.OnShift += new LALRParser.ShiftHandler(ShiftEvent);
					parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
					parser.OnGoto += new LALRParser.GotoHandler(GotoEvent);
					parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
					parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
					parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
					parser.OnCommentRead += new LALRParser.CommentReadHandler(CommentReadEvent);
					long t2 = DateTime.Now.Ticks;
					WriteLn(String.Format("Reading the Compiled Grammar Table File took {0}ms",(t2-t1)/10000));
				}
				catch (Exception e)
				{
					WriteLn(e.Message);
				}
			}

		}

		private void OpenSource() 
		{
			if (openSourceDialog.ShowDialog() == DialogResult.OK) 
			{
				try
				{
					settings.LastSource = openSourceDialog.FileName;
					SaveSettings();
					RichTextBoxStreamType type;
					if (FileUtil.IsUTF16LE(openSourceDialog.FileName))
						type = RichTextBoxStreamType.UnicodePlainText;
					else
						type = RichTextBoxStreamType.PlainText;
					sourceTextBox.LoadFile(openSourceDialog.FileName, type);
					if (type == RichTextBoxStreamType.UnicodePlainText)
						sourceTextBox.Text = sourceTextBox.Text.Remove(0, 1);
				}
				catch (IOException e)
				{
					MessageBox.Show(this, e.Message, "Error opening file");
				}
			}
		}

		private void ParseSource()
		{
			if (parser != null)
			{
				parser.TrimReductions = settings.TrimReductions;
				errors = 0;
				parseActionsView.Items.Clear();
				parseTreeView.Nodes.Clear();
				tabControl.SelectedIndex = 1;

				//reader.LALRParser.Parse("abc 123 a34   (*@@@@@ *) last	text");
				//parser.Parse("aaa//comment1\nbbb\n//comm(*ent*)2\nccc");
				WriteLn("Parsing source...");
				long t1 = DateTime.Now.Ticks;
				try
				{
					string str = sourceTextBox.Text;
					//str = str.Replace("\n","\r\n");
					parser.Parse(str);

					long t2 = DateTime.Now.Ticks;
					WriteLn(String.Format("Parsing the source took {0}ms",(t2-t1)/10000));
				}
				catch (System.ApplicationException e)
				{
					WriteLn(e.Message);
					AddViewItem("Internal error",null,"View log for details","","",5);
				}
			}
		}

        private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
			if (e.Button == openGrammarButton)
			{
				OpenGrammar();
			}
			else if (e.Button == logButton)
			{
				ActivateLog();
			}
			else if (e.Button == testGrammarButton)
			{
				ActivateTest();
			}
			else if (e.Button == exitButton)
			{
				Application.Exit();
			}
        }

		private void openGrammarClick(object sender, System.EventArgs e)
		{
			OpenGrammar();
		}

		private void exitClick(object sender, System.EventArgs e)
		{	 
			Application.Exit();
		}

		private void aboutClick(object sender, System.EventArgs e)
		{
			Form aboutForm = new AboutForm();
			aboutForm.ShowDialog();

			/*
			String message =
				"Calitha GOLD Parser Engine Test 1.9\n\n"+
				"Author: Robert van Loenhout\n\n"+
				"Website: http://www.calitha.com\n\n"+
				"For more information about the GOLD parser see http://www.goldparser.com\n\n";

			MessageBox.Show(message, "About Gold Parser Engine Test", 
				MessageBoxButtons.OK, MessageBoxIcon.Information);*/
		}

		private void testGrammarClick(object sender, System.EventArgs e)
		{
			ActivateTest();
		}

		private void viewLogClick(object sender, System.EventArgs e)
		{
			ActivateLog();
		}

		private void parseButton_Click(object sender, System.EventArgs e)
		{
			ParseSource();
		}

		private void trimReductionsBox_CheckedChanged(object sender, System.EventArgs e)
		{
			settings.TrimReductions = trimReductionsBox.Checked;
			SaveSettings();
		}

		private void maxErrorsBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string maxerrorsstr = maxErrorsBox.Items[maxErrorsBox.SelectedIndex].ToString();
				maxerrors = Int32.Parse(maxerrorsstr);
				settings.MaxErrorsIndex = maxErrorsBox.SelectedIndex;
				SaveSettings();
			}
			catch
			{
				maxerrors = 0;
			}
		}

		private void setFontMenuItem_Click(object sender, System.EventArgs e)
		{
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{
				settings.Font = fontDialog.Font;
				SetFontInViews();
				SaveSettings();
			}
		}

		private void SetFontInViews()
		{
			logBox.Font = settings.Font;
			sourceTextBox.Font = settings.Font;
			parseActionsView.Font = settings.Font;
			parseTreeView.Font = settings.Font;
		}

		private void openSourceButton_Click(object sender, System.EventArgs e)
		{
			OpenSource();
		}

	}
}
