using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.calitha.commons;

namespace com.calitha.goldparser.test
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox aboutBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
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
				if(components != null)
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
            this.aboutBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // aboutBox
            // 
            this.aboutBox.BackColor = System.Drawing.Color.White;
            this.aboutBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.aboutBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.aboutBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aboutBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.aboutBox.Location = new System.Drawing.Point(10, 10);
            this.aboutBox.Name = "aboutBox";
            this.aboutBox.ReadOnly = true;
            this.aboutBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.aboutBox.Size = new System.Drawing.Size(330, 244);
            this.aboutBox.TabIndex = 0;
            this.aboutBox.TabStop = false;
            this.aboutBox.Text = "";
            this.aboutBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.aboutBox_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 264);
            this.Controls.Add(this.aboutBox);
            this.DockPadding.All = 10;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);

        }
		#endregion

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			aboutBox.Rtf = ResourceUtil.GetStringResource
				(System.Reflection.Assembly.GetExecutingAssembly(),
                 "com.calitha.goldparser.test.application", 
                 "about");
		}

		private void aboutBox_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start(e.LinkText);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Unable to start default browser",  
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
