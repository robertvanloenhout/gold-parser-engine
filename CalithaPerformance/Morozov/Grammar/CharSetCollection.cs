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
using System.Collections;

namespace Morozov
{
	namespace GoldParser
	{
		/// <summary>
		/// Contains read only collection of char sets.
		/// </summary>
		public class CharSetCollection : IEnumerable
		{
			private string[] m_charSetTable;

			/// <summary>
			/// Creates a new instance of <c>CharSetCollection</c> class.
			/// </summary>
			/// <param name="strings">
			/// Array of char set strings to initialize the collection.
			/// </param>
			public CharSetCollection(string[] strings)
			{
				m_charSetTable = strings;
			}

			/// <summary>
			/// Gets a char set by its index.
			/// </summary>
			public string this[int index]
			{
				get { return m_charSetTable[index]; }
			}

			/// <summary>
			/// Gets number of char set strings.
			/// </summary>
			public int Count
			{
				get { return m_charSetTable.Length; }
			}

			#region IEnumerable Members

			/// <summary>
			/// Creates a new IEnumerator for the collection.
			/// </summary>
			/// <returns>New instance of IEnumerator.</returns>
			public IEnumerator GetEnumerator()
			{
				return m_charSetTable.GetEnumerator();
			}

			#endregion
		}
	}
}