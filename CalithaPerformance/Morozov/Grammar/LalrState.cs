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

namespace Morozov
{
	namespace GoldParser
	{
		/// <summary>
		/// State of LALR parser.
		/// </summary>
		public class LalrState
		{
			private int m_index;
			private LalrStateAction[] m_actions;
			private LalrStateAction[] m_transitionVector;

			/// <summary>
			/// Creates a new instance of the <c>LalrState</c> class
			/// </summary>
			/// <param name="index">Index of the LALR state in the LALR state table.</param>
			/// <param name="actions">List of all available LALR actions in this state.</param>
			/// <param name="transitionVector">Transition vector which has symbol index as an index.</param>
			public LalrState(int index, LalrStateAction[] actions, LalrStateAction[] transitionVector)
			{
				m_index = index;
				m_actions = actions;
				m_transitionVector = transitionVector;
			}

			/// <summary>
			/// Gets index of the LALR state in LALR state table.
			/// </summary>
			public int Index
			{
				get { return m_index; }
			}

			/// <summary>
			/// Gets LALR state action count.
			/// </summary>
			public int ActionCount
			{
				get { return m_actions.Length; }
			}

			/// <summary>
			/// Returns state action by its index.
			/// </summary>
			/// <param name="index">State action index.</param>
			/// <returns>LALR state action for the given index.</returns>
			public LalrStateAction GetAction(int index)
			{
				return m_actions[index];
			}

			/// <summary>
			/// Returns LALR state action by symbol index.
			/// </summary>
			/// <param name="symbolIndex">Symbol Index to search for.</param>
			/// <returns>LALR state action object.</returns>
			public LalrStateAction GetActionBySymbolIndex(int symbolIndex)
			{
				return m_transitionVector[symbolIndex];
			}
		}
	}
}