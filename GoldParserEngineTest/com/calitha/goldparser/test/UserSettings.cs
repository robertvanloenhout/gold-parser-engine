using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.calitha.goldparser.test
{
	/// <summary>
	/// A UserSettings object represents the current settings
	/// for this application.
	/// </summary>
	[Serializable]
	public class UserSettings
	{
		private string lastGrammar;
		private string lastSource;
		private Font font;
		private int maxErrorsIndex;
		private bool trimReductions;

		/// <summary>
		/// Creates new default settings.
		/// </summary>
		public UserSettings()
		{
			lastGrammar = "";
			lastSource = "";
			font = null;
			maxErrorsIndex = 0;
			trimReductions = false;
		}

		/// <summary>
		/// Saves the settings to a file.
		/// </summary>
		/// <param name="filename">filename</param>
		public void Save(string filename)
		{
			FileStream fs = new FileStream(filename, FileMode.Create);
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, this);
			}
			finally
			{
				fs.Close();
			}
		}

		/// <summary>
		/// Loads the settings from a file.
		/// </summary>
		/// <param name="filename">filename</param>
		/// <returns>settings</returns>
		public static UserSettings Load(string filename)
		{
			FileStream fs = new FileStream(filename, FileMode.Open);
			UserSettings result;
			try
			{
				BinaryFormatter formatter = new BinaryFormatter();
				result = (UserSettings)formatter.Deserialize(fs);
			}
			finally
			{
				fs.Close();
			}
			return result;
		}

		/// <summary>
		/// Gets the filename of the last opened CGT file.
		/// </summary>
		/// <returns>filename</returns>
		public String GetLastGrammar()
		{
			return lastGrammar;
		}

		/// <summary>
		/// Sets the filename of the last openened CGT file.
		/// </summary>
		/// <param name="lastGrammar">filename</param>
		public void SetLastGrammar(string lastGrammar)
		{
			this.lastGrammar = lastGrammar;
		}

		/// <summary>
		/// Gets the last openened source file.
		/// </summary>
		/// <returns>filename</returns>
		public String GetLastSource()
		{
			return lastSource;
		}

		/// <summary>
		/// Sets the last opened source file.
		/// </summary>
		/// <param name="lastSource">filename</param>
		public void SetLastSource(string lastSource)
		{
			this.lastSource = lastSource;
		}

		/// <summary>
		/// Gets the font for the user interface.
		/// </summary>
		/// <returns>font</returns>
		public Font GetFont()
		{
			return font;
		}

		/// <summary>
		/// Sets the font for the user interface.
		/// </summary>
		/// <param name="font">font</param>
		public void SetFont(Font font)
		{
			this.font = font;
		}

		/// <summary>
		/// Gets the maximum errors combobox index.
		/// </summary>
		/// <returns>index</returns>
		public int GetMaxErrorsIndex()
		{
			return maxErrorsIndex;
		}

		/// <summary>
		/// Sets the maximum errors combobox index.
		/// </summary>
		/// <param name="maxErrorsIndex">index</param>
		public void SetMaxErrorsIndex(int maxErrorsIndex)
		{
			this.maxErrorsIndex = maxErrorsIndex;
		}

		/// <summary>
		/// Gets the trim reductions mode.
		/// </summary>
		/// <returns>trim reductions mode</returns>
		public bool GetTrimReductions()
		{
			return trimReductions;
		}

		/// <summary>
		/// Sets the trim reductions mode.
		/// </summary>
		/// <param name="trimReductions">trim reductions mode</param>
		public void SetTrimReductions(bool trimReductions)
		{
			this.trimReductions = trimReductions;
		}

		/// <summary>
		/// Last opened CGT file.
		/// </summary>
		public string LastGrammar
		{
			get {return GetLastGrammar();}
			set {SetLastGrammar(value);}
		}

		/// <summary>
		/// Last opened source file.
		/// </summary>
		public string LastSource
		{
			get {return GetLastSource();}
			set {SetLastSource(value);}
		}

		/// <summary>
		/// User interface font.
		/// </summary>
		public Font Font
		{
			get {return GetFont();}
			set {SetFont(value);}
		}

		/// <summary>
		/// Maximum errors combobox index.
		/// </summary>
		public int MaxErrorsIndex
		{
			get {return GetMaxErrorsIndex();}
			set {SetMaxErrorsIndex(value);}
		}

		/// <summary>
		/// Trim reductions mode.
		/// </summary>
		public bool TrimReductions
		{
			get {return GetTrimReductions();}
			set {SetTrimReductions(value);}
		}

	}
}
