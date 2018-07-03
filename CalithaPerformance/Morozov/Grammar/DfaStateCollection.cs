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
		/// Contains read only collection of DFA states.
		/// </summary>
		public class DfaStateCollection : IEnumerable
		{
			private DfaState[] m_dfaStateTable;

			/// <summary>
			/// Creates a new instance of <c>DfaStateCollection</c> class.
			/// </summary>
			/// <param name="dfaStateTable">
			/// Array of DFA states to initialize the collection.
			/// </param>
			public DfaStateCollection(DfaState[] dfaStateTable)
			{
				m_dfaStateTable = dfaStateTable;
			}

			/// <summary>
			/// Gets DFA state by its index.
			/// </summary>
			public DfaState this[int index]
			{
				get { return m_dfaStateTable[index]; }
			}

			/// <summary>
			/// Gets number of DFA states.
			/// </summary>
			public int Count
			{
				get { return m_dfaStateTable.Length; }
			}

			#region IEnumerable Members

			/// <summary>
			/// Creates a new IEnumerator for the collection.
			/// </summary>
			/// <returns>New instance of IEnumerator.</returns>
			public IEnumerator GetEnumerator()
			{
				return m_dfaStateTable.GetEnumerator();
			}

			#endregion
		}
	}
}