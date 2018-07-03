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
		/// Contains read only collection of LALR states.
		/// </summary>
		public class LalrStateCollection : IEnumerable
		{
			private LalrState[] m_lalrStateTable;

			/// <summary>
			/// Creates a new instance of <c>LalrStateCollection</c> class.
			/// </summary>
			/// <param name="lalrStateTable">
			/// Array of LALR states to initialize the collection.
			/// </param>
			public LalrStateCollection(LalrState[] lalrStateTable)
			{
				m_lalrStateTable = lalrStateTable;
			}

			/// <summary>
			/// Gets LALR state by its index.
			/// </summary>
			public LalrState this[int index]
			{
				get { return m_lalrStateTable[index]; }
			}

			/// <summary>
			/// Gets number of LALR states.
			/// </summary>
			public int Count
			{
				get { return m_lalrStateTable.Length; }
			}

			#region IEnumerable Members

			/// <summary>
			/// Creates a new IEnumerator for the collection.
			/// </summary>
			/// <returns>New instance of IEnumerator.</returns>
			public IEnumerator GetEnumerator()
			{
				return m_lalrStateTable.GetEnumerator();
			}

			#endregion
		}
	}
}