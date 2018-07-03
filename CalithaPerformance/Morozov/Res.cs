//----------------------------------------------------------------------
// Gold Parser engine.
// See more details on http://www.devincook.com/goldparser/
// 
// Original code is written in VB by Devin Cook (GOLDParser@DevinCook.com)
//
// This translation is done by Vladimir Morozov (vmoroz@hotmail.com)
// 
// The translation is based on the other engine translations:
// Delphi engine by Alexandre Rai (riccio@gmx.at)
// C# engine by Marcus Klimstra (klimstra@home.nl)
//----------------------------------------------------------------------

using System;
using System.IO;
using System.Resources;
using System.Globalization;

namespace Morozov
{
	namespace GoldParser
	{
		/// <summary>
		/// Custom resource class. Usage:
		/// string s = Res.GetString(Res.MyIdenfitier);
		/// </summary>
		internal sealed class Res
		{
			static Res ms_loader = null;

			private ResourceManager m_resources;

			private Res()
			{
				m_resources = new ResourceManager("GoldParser", this.GetType().Module.Assembly);
			}

			private static Res GetLoader()
			{
				if (ms_loader == null)
				{
					lock (typeof(Res))
					{
						if (ms_loader == null)
						{
							ms_loader = new Res();
						}
					}
				}
				return ms_loader;
			}

			/* These function can be useful in an other application.
		 		
			public static string GetString(string name, params object[] args)
			{
				// null CultureInfo: let ResouceManager determine the culture
				return GetString(null, name, args);
			}

			public static string GetString(CultureInfo culture, string name, params object[] args)
			{
				Res loader = GetLoader();
				if (loader == null)
				{
					return null;
				}
				string res = loader.m_resources.GetString(name, culture);

				if (args != null && args.Length > 0)
				{
					return String.Format(culture, res, args);
				}
				else
				{
					return res;
				}
			}
	*/
			public static string GetString(string name)
			{
				return GetString(null, name);
			}

			public static string GetString(CultureInfo culture, string name)
			{
				Res loader = GetLoader();
				if (loader == null)
				{
					return null;
				}
				return loader.m_resources.GetString(name, culture);
			}

			// Code below is automatically generated by GenResNm.js script.
			// Do not modify it manually.
			#region Resource String Names

			internal const string Grammar_WrongFileHeader = "Grammar_WrongFileHeader";

			internal const string Grammar_InvalidRecordType = "Grammar_InvalidRecordType";

			internal const string Grammar_NoEntry = "Grammar_NoEntry";

			internal const string Grammar_EmptyEntryExpected = "Grammar_EmptyEntryExpected";

			internal const string Grammar_StringEntryExpected = "Grammar_StringEntryExpected";

			internal const string Grammar_IntegerEntryExpected = "Grammar_IntegerEntryExpected";

			internal const string Grammar_ByteEntryExpected = "Grammar_ByteEntryExpected";

			internal const string Grammar_BooleanEntryExpected = "Grammar_BooleanEntryExpected";

			internal const string Grammar_InvalidRecordHeader = "Grammar_InvalidRecordHeader";

			#endregion
		}
	}
}