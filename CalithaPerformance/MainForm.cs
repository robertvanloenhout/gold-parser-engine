using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using com.calitha.goldparser;
using System.IO;
using com.calitha.commons;
using Morozov.GoldParser;


	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox cgtEdit;
		private System.Windows.Forms.TextBox sourceEdit;
		private System.Windows.Forms.Button goButton;
		private System.Windows.Forms.RichTextBox outputBox;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.cgtEdit = new System.Windows.Forms.TextBox();
			this.sourceEdit = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.goButton = new System.Windows.Forms.Button();
			this.outputBox = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cgtEdit
			// 
			this.cgtEdit.Location = new System.Drawing.Point(112, 16);
			this.cgtEdit.Name = "cgtEdit";
			this.cgtEdit.Size = new System.Drawing.Size(280, 20);
			this.cgtEdit.TabIndex = 0;
			this.cgtEdit.Text = "E:\\projects\\DotNet\\CalithaPerformance\\sqlselect.cgt";
			// 
			// sourceEdit
			// 
			this.sourceEdit.Location = new System.Drawing.Point(112, 56);
			this.sourceEdit.Name = "sourceEdit";
			this.sourceEdit.Size = new System.Drawing.Size(280, 20);
			this.sourceEdit.TabIndex = 1;
			this.sourceEdit.Text = "E:\\projects\\DotNet\\CalithaPerformance\\query.sql";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "CGT file:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Source file:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// goButton
			// 
			this.goButton.Location = new System.Drawing.Point(144, 112);
			this.goButton.Name = "goButton";
			this.goButton.TabIndex = 4;
			this.goButton.Text = "Test Calitha";
			this.goButton.Click += new System.EventHandler(this.goButton_Click);
			// 
			// outputBox
			// 
			this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.outputBox.Location = new System.Drawing.Point(8, 168);
			this.outputBox.Name = "outputBox";
			this.outputBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.outputBox.Size = new System.Drawing.Size(472, 120);
			this.outputBox.TabIndex = 5;
			this.outputBox.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(272, 112);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "Test Morozov";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 302);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.outputBox);
			this.Controls.Add(this.goButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.sourceEdit);
			this.Controls.Add(this.cgtEdit);
			this.Name = "MainForm";
			this.Text = "Calitha Performance";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void goButton_Click(object sender, System.EventArgs e)
		{
			testCalithaEngine(this.cgtEdit.Text, this.sourceEdit.Text);
			/*
			ISet set = new HashSet();
			set.Add(5);
			IEnumerator values = set.GetEnumerator();
			bool b = values.MoveNext();
			Object obj = values.Current;
			outputBox.AppendText(obj.ToString());
			*/
		}

		public void testCalithaEngine(string cgtFile, string sourceFile)
		{
			FileStream fs = new FileStream(cgtFile, FileMode.Open, FileAccess.Read);
			CGTReader reader = new CGTReader(fs);
			LALRParser parser = reader.CreateNewParser();
			parser.StoreTokens = LALRParser.StoreTokensMode.Never;
			fs.Close();

			FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(sourceStream);
			String source = sr.ReadToEnd();

			parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
			parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);

			TimeSpan timeStart = new TimeSpan(DateTime.Now.Ticks);

			parser.Parse(source);
			/*
			for (int i = 0; i < 1000; i++)
			{
				parser.Parse(source);
			}
			*/
			TimeSpan timeEnd = new TimeSpan(DateTime.Now.Ticks);

			this.outputBox.AppendText("Took "+timeEnd.Subtract(timeStart).ToString()+"\n");

		}

		public void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
		{
			outputBox.AppendText("token error\n");
		}

		public void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
		{
			outputBox.AppendText("Parse error\n");
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			testMorozovEngine(this.cgtEdit.Text, this.sourceEdit.Text);

		}

		public void testMorozovEngine(string cgtFile, string sourceFile)
		{
			FileStream fs = new FileStream(cgtFile, FileMode.Open, FileAccess.Read);
			BinaryReader reader = new BinaryReader(fs);
			Grammar grammar = new Grammar(reader);
			FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(sourceStream);
			String source = sr.ReadToEnd();
			TimeSpan timeStart = new TimeSpan(DateTime.Now.Ticks);

			Parser parser = new Parser(source, grammar);
			ParseMessage response = ParseMessage.Empty;
			while (response != ParseMessage.Accept)
			{
				response = parser.Parse();
			}
			/*
			for (int i = 0; i < 1000; i++)
			{
				Parser parser = new Parser(source, grammar);
				ParseMessage response = ParseMessage.Empty;
				while (response != ParseMessage.Accept)
				{
					response = parser.Parse();
				}
			}
			*/
			TimeSpan timeEnd = new TimeSpan(DateTime.Now.Ticks);

			this.outputBox.AppendText("Took "+timeEnd.Subtract(timeStart).ToString()+"\n");

		}
			

	}
