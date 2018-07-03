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
		/// State in the Deterministic Finite Automata 
		/// which is used by the tokenizer.
		/// </summary>
		public class DfaState
		{
			private int m_index;
			private Symbol m_acceptSymbol;
			private Hashtable m_transitionVector;

			/// <summary>
			/// Creates a new instance of the <c>DfaState</c> class.
			/// </summary>
			/// <param name="index">Index in the DFA state table.</param>
			/// <param name="acceptSymbol">Symbol to accept.</param>
			/// <param name="transitionVector">Transition vector.</param>
			public DfaState(int index, Symbol acceptSymbol, Hashtable transitionVector)
			{
				m_index = index;
				m_acceptSymbol = acceptSymbol;
				m_transitionVector = transitionVector;
			}

			/// <summary>
			/// Gets index of the state in DFA state table.
			/// </summary>
			public int Index
			{
				get { return m_index; }
			}

			/// <summary>
			/// Gets the symbol which can be accepted in this DFA state.
			/// </summary>
			public Symbol AcceptSymbol
			{
				get { return m_acceptSymbol; }
			}

			/// <summary>
			/// Returns next DFA state by the given character.
			/// </summary>
			/// <param name="value">Character to choose next DFA state.</param>
			/// <returns>Index of DFA state in DFA state table.</returns>
			public int GetNextState(char value)
			{
				object nextState = m_transitionVector[value];
				if (nextState != null)
				{
					return (int)nextState;
				}
				return -1;
			}
		}
	}
}