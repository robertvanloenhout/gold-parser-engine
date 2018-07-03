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
		/// Reads tokens from the CharReader using grammar rules.
		/// </summary>
		public class TokenReader
		{
			private CharReader m_charReader;      // source of the data
			private DfaStateCollection m_dfaStateTable;   // table of DFA states 
			private SymbolCollection m_symbolTable;     // symbol table.
			private Symbol m_errorSymbol;     // Special error symbol
			private Symbol m_endSymbol;       // Special end symbol
			private int m_dfaInitialState; // initial automata state.

			/// <summary>
			/// Creates a new instance of the <c>TokenReader</c> class.
			/// </summary>
			/// <param name="reader">Source to parse.</param>
			/// <param name="grammar">Grammar rules.</param>
			public TokenReader(CharReader reader, Grammar grammar)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("charReader");
				}
				if (grammar == null)
				{
					throw new ArgumentNullException("grammar");
				}

				m_charReader = reader;
				m_dfaStateTable = grammar.DfaStateTable;
				m_symbolTable = grammar.SymbolTable;
				m_dfaInitialState = grammar.DfaInitialState;
				// Find special symbols.
				foreach (Symbol symbol in m_symbolTable)
				{
					switch (symbol.SymbolType)
					{
						case SymbolType.Error:
							m_errorSymbol = symbol;
							break;

						case SymbolType.End:
							m_endSymbol = symbol;
							break;
					}
				}
			}

			/// <summary>
			/// Reads the next token.
			/// </summary>
			/// <returns>Read token.</returns>
			public Token ReadToken()
			{
				//The first state is almost always #1.
				int currentDFA = m_dfaInitialState;
				int charShift = 0;  // Next byte in the input CharReader
				int lastAcceptState = -1; // We have not yet accepted a character string
				int acceptedChars = 0;

				char ch = m_charReader.CurrentChar;
				if (ch != CharReader.EndOfString)
				{
					while (true)
					{
						ch = m_charReader.GetChar(charShift);
						int target = m_dfaStateTable[currentDFA].GetNextState(ch);

						// This block-if statement checks whether an edge was found from the current state.
						// If so, the state and current position advance. Otherwise it is time to exit the main loop
						// and report the token found (if there was it fact one). If the LastAcceptState is -1,
						// then we never found a match and the Error Token is created. Otherwise, a new token
						// is created using the Symbol in the Accept State and all the characters that
						// comprise it.
						if (target >= 0)
						{
							// This code checks whether the target state accepts a token. If so, it sets the
							// appropiate variables so when the algorithm in done, it can return the proper
							// token and number of characters.
							if (m_dfaStateTable[target].AcceptSymbol != null)
							{
								lastAcceptState = target;
								acceptedChars = charShift + 1;
							}
							currentDFA = target;
							charShift++;
						}
						else
						{
							//No edge found
							if (lastAcceptState == -1)
							{
								//Tokenizer cannot recognize symbol
								return new Token(m_errorSymbol, m_charReader.Accept(), LineNumber);
							}
							else
							{
								//Create Token, read characters
								Symbol tokenSymbol = m_dfaStateTable[lastAcceptState].AcceptSymbol;
								//The data contains the total number of accept characters
								string data = m_charReader.Accept(acceptedChars);
								return new Token(tokenSymbol, data, LineNumber);
							}
						}
					}
				}
				else
				{
					return new Token(m_endSymbol, "", LineNumber);
				}
			}

			/// <summary>
			/// Reads the source string to the end of the line.
			/// </summary>
			/// <returns>Read string.</returns>
			public string ReadToLineEnd()
			{
				return m_charReader.AcceptToLineEnd();
			}

			/// <summary>
			/// Gets current line number.
			/// </summary>
			public int LineNumber
			{
				get { return m_charReader.LineNumber; }
			}
		}
	}
}