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
		/// Contains read only collection of symbols.
		/// </summary>
		public class SymbolCollection : IEnumerable
		{
			private Symbol[] m_symbolTable;

			/// <summary>
			/// Creates a new instance of <c>SymbolCollection</c> class.
			/// </summary>
			/// <param name="symbolTable">
			/// Array of symbols to initialize the collection.
			/// </param>
			public SymbolCollection(Symbol[] symbolTable)
			{
				m_symbolTable = symbolTable;
			}

			/// <summary>
			/// Gets a symbol by its index.
			/// </summary>
			public Symbol this[int index]
			{
				get { return m_symbolTable[index]; }
			}

			/// <summary>
			/// Gets number of symbols.
			/// </summary>
			public int Count
			{
				get { return m_symbolTable.Length; }
			}

			#region IEnumerable Members

			/// <summary>
			/// Creates a new IEnumerator for the collection.
			/// </summary>
			/// <returns>New instance of IEnumerator.</returns>
			public IEnumerator GetEnumerator()
			{
				return m_symbolTable.GetEnumerator();
			}

			#endregion
		}
	}
}