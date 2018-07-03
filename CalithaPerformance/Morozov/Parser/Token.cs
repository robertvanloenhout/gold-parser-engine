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
using System.Text;

namespace Morozov
{
	namespace GoldParser
	{
		/// <summary>
		/// Represents individual syntactic units.
		/// </summary>
		/// <remarks>
		/// While the Symbol represents a class of terminals and nonterminals, 
		/// the Token represents an individual piece of information.
		/// </remarks>
		public class Token
		{
			private Symbol m_symbol;
			private string m_data;
			private Reduction m_reduction;
			private LalrState m_state;
			private int m_lineNumber = 0;

			/// <summary>
			/// Create a new instance of <c>Token</c> class.
			/// </summary>
			/// <param name="symbol">Symbol associated with the token.</param>
			/// <param name="data">Source text associated with the token.</param>
			/// <param name="lineNumber">Line number where the token parsed.</param>
			public Token(Symbol symbol, string data, int lineNumber)
			{
				m_symbol = symbol;
				m_data = data;
				m_lineNumber = lineNumber;
			}

			/// <summary>
			/// Create a new instance of <c>Token</c> class.
			/// </summary>
			/// <param name="symbol">Symbol associated with the token.</param>
			/// <param name="reduction">Reduction associated with the token.</param>
			public Token(Symbol symbol, Reduction reduction)
			{
				m_symbol = symbol;
				m_reduction = reduction;
			}

			/// <summary>
			/// Gets the line number for the token.
			/// </summary>
			public int LineNumber
			{
				get
				{
					if (m_reduction != null)
					{
						return m_reduction.LineNumber;
					}
					return m_lineNumber;
				}
			}

			/// <summary>
			/// Gets the token data string.
			/// </summary>
			public string Data
			{
				get { return m_data; }
			}

			/// <summary>
			/// Gets or sets the token symbol.
			/// </summary>
			public Symbol Symbol
			{
				get { return m_symbol; }
				set { m_symbol = value; }
			}

			/// <summary>
			/// Gets or sets the token reduction.
			/// </summary>
			public Reduction Reduction
			{
				get { return m_reduction; }
				set { m_reduction = value; }
			}

			/// <summary>
			/// Gets or sets the LALR state for the token.
			/// </summary>
			public LalrState LalrState
			{
				get { return m_state; }
				set { m_state = value; }
			}

			/// <summary>
			/// Gets the symbol type.
			/// </summary>
			public SymbolType SymbolType
			{
				get { return Symbol.SymbolType; }
			}

			/// <summary>
			/// Gets the symbol name.
			/// </summary>
			public string Name
			{
				get { return Symbol.Name; }
			}

			/// <summary>
			/// Returns string representation of the token.
			/// </summary>
			/// <returns>String representation of the token.</returns>
			public override string ToString()
			{
				if (SymbolType != SymbolType.Terminal)
				{
					return Symbol.ToString();
				}
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < m_data.Length; i++)
				{
					char ch = m_data[i];
					if (ch < ' ')
					{
						switch (ch)
						{
							case (char)0xA:
								sb.Append("{LF}");
								break;
							case (char)0xD:
								sb.Append("{CR}");
								break;
							case (char)0x9:
								sb.Append("{HT}");
								break;
						}
					}
					else
					{
						sb.Append(ch);
					}
				}
				return sb.ToString();
			}
		}
	}
}