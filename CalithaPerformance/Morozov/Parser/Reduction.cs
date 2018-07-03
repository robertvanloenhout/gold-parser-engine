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
		/// Represents reduced rule.
		/// </summary>
		/// <remarks>
		/// This class is used by the engine to hold a reduced rule. Rather the contain
		/// a list of Symbols, a reduction contains a list of Tokens corresponding to the
		/// the rule it represents. 
		/// </remarks>
		public class Reduction
		{
			private Rule m_rule;
			private ArrayList m_items = new ArrayList();
			private int m_number;

			/// <summary>
			/// Creates a new instance of the <c>Reduction</c> class.
			/// </summary>
			/// <param name="rule">The rule on which the reduction is based.</param>
			public Reduction(Rule rule)
			{
				m_rule = rule;
			}

			/// <summary>
			/// Adds a token to thetoken list.
			/// </summary>
			/// <param name="index">Index to insert the token.</param>
			/// <param name="token">Token to insert.</param>
			public void InsertToken(int index, Token token)
			{
				m_items.Insert(index, token);
			}

			/// <summary>
			/// Gets the reduction line number in the source file.
			/// </summary>
			public int LineNumber
			{
				get
				{
					if (m_items.Count > 0)
					{
						return ((Token)m_items[0]).LineNumber;
					}
					return -1;
				}
			}

			/// <summary>
			/// Gets number of tokens.
			/// </summary>
			public int Count
			{
				get { return m_items.Count; }
			}

			/// <summary>
			/// Gets a token by its index.
			/// </summary>
			public Token this[int index]
			{
				get { return (Token)m_items[index]; }
			}

			/// <summary>
			/// Gets or sets reduction numebr.
			/// </summary>
			public int ReductionNumber
			{
				get { return m_number; }
				set { m_number = value; }
			}

			/// <summary>
			/// Gets the reduction rule.
			/// </summary>
			public Rule Rule
			{
				get { return m_rule; }
			}
		}
	}
}