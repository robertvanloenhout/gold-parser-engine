using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Resources;
using com.calitha.textcalc.expression;
using com.calitha.goldparser;
using com.calitha.commons;

namespace com.calitha.textcalc
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		public static MainForm mainForm = new MainForm();
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RichTextBox inputBox;
		private System.Windows.Forms.RichTextBox outputBox;
		private System.Windows.Forms.CheckBox afterAcceptBox;
		private System.Windows.Forms.RichTextBox helpBox;

		private IParser parser;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			parser = new CalcParserV2("com.calitha.textcalc.calculator","grammar");


			/*
			CGTReader reader = new CGTReader(stream);
			parser = reader.CreateNewParser();
			parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
			parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
			parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
			parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
			parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
			*/
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.panel1 = new System.Windows.Forms.Panel();
			this.helpBox = new System.Windows.Forms.RichTextBox();
			this.outputBox = new System.Windows.Forms.RichTextBox();
			this.inputBox = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.afterAcceptBox = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 376);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(520, 22);
			this.statusBar1.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.helpBox);
			this.panel1.Controls.Add(this.outputBox);
			this.panel1.Controls.Add(this.inputBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.afterAcceptBox);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 376);
			this.panel1.TabIndex = 5;
			// 
			// helpBox
			// 
			this.helpBox.Location = new System.Drawing.Point(152, 8);
			this.helpBox.Name = "helpBox";
			this.helpBox.ReadOnly = true;
			this.helpBox.Size = new System.Drawing.Size(360, 96);
			this.helpBox.TabIndex = 11;
			this.helpBox.Text = "The Text Calculator allows the following content:\nInteger values (examples: -1, 9" +
				"99)\nFloat values (examples -5.0, .2)\nPlus operator: +\nSubtract operator: -\nMulti" +
				"ply operator: *\nDivide operator: / (6/4 = 1, 6.0/4 = 1.5)";
			// 
			// outputBox
			// 
			this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.outputBox.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.outputBox.Location = new System.Drawing.Point(8, 296);
			this.outputBox.Name = "outputBox";
			this.outputBox.ReadOnly = true;
			this.outputBox.Size = new System.Drawing.Size(504, 72);
			this.outputBox.TabIndex = 10;
			this.outputBox.Text = "";
			// 
			// inputBox
			// 
			this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.inputBox.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.inputBox.ForeColor = System.Drawing.Color.ForestGreen;
			this.inputBox.Location = new System.Drawing.Point(8, 160);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(504, 72);
			this.inputBox.TabIndex = 9;
			this.inputBox.Text = "";
			this.inputBox.TextChanged += new System.EventHandler(this.inputBox_TextChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 240);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(488, 48);
			this.label2.TabIndex = 7;
			this.label2.Text = "RESULT";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 112);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(488, 40);
			this.label1.TabIndex = 6;
			this.label1.Text = "INPUT";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// afterAcceptBox
			// 
			this.afterAcceptBox.Location = new System.Drawing.Point(16, 8);
			this.afterAcceptBox.Name = "afterAcceptBox";
			this.afterAcceptBox.Size = new System.Drawing.Size(136, 24);
			this.afterAcceptBox.TabIndex = 9;
			this.afterAcceptBox.Text = "Calculate after accept";
			this.afterAcceptBox.CheckedChanged += new System.EventHandler(this.afterAcceptBox_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 398);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.statusBar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Calitha Text Calculator";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.Run(new MainForm());
			Application.Run(mainForm);
		}

		public void WriteResult(string message)
		{
			this.outputBox.Text = message;
		}

		public void ShowInputError(TerminalToken token)
		{
			ClearShowInputError();
			int oldpos = this.inputBox.SelectionStart;
			this.inputBox.Select(token.Location.Position, token.Text.Length);
			this.inputBox.SelectionColor = Color.Red;
			this.inputBox.Select(oldpos,0);
			this.inputBox.SelectionColor = Color.ForestGreen;
		}

		public void ClearShowInputError()
		{
			int oldpos = this.inputBox.SelectionStart;
			this.inputBox.SelectAll();
			this.inputBox.SelectionColor = Color.ForestGreen;
			this.inputBox.Select(oldpos,0);
			this.inputBox.SelectionColor = Color.ForestGreen;
		}

		private void inputBox_TextChanged(object sender, System.EventArgs e)
		{
			parser.Parse(inputBox.Text);
		}

		private void afterAcceptBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.afterAcceptBox.Checked)
			{
				parser = new CalcParserV2("com.calitha.textcalc.calculator","grammar");
			}
			else
			{
				parser = new CalcParserV1("com.calitha.textcalc.calculator","grammar");
			}
		}
	}
}
