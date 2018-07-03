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
		/// Parser class.
		/// </summary>
		public class Parser
		{
			private Grammar m_grammar;
			private TokenStack m_stack = new TokenStack();
			private TokenStack m_tokens = new TokenStack();
			private TokenStack m_inputTokens = new TokenStack();
			private LalrState m_currentLalrState;
			private bool m_haveReduction = false;
			private TokenReader m_tokenReader;
			private int m_commentLevel = 0;
			private Hashtable m_comments = new Hashtable();
			private bool m_trimReductions;

			/// <summary>
			/// Creates a new instance of the <c>Parser</c> class.
			/// </summary>
			/// <param name="input">String to parse.</param>
			/// <param name="grammar">Grammar rules.</param>
			public Parser(string input, Grammar grammar)
			{
				m_grammar = grammar;
				CharReader charReader = new CharReader(input);
				m_tokenReader = new TokenReader(charReader, grammar);
				m_currentLalrState = m_grammar.InitialLalrState;
				Symbol symbol = m_grammar.StartSymbol;
				Token start = new Token(symbol, (Reduction)null);
				start.LalrState = m_currentLalrState;
				m_stack.Push(start);
			}

			/// <summary>
			/// Gets the parser's grammar.
			/// </summary>
			public Grammar Grammar
			{
				get { return m_grammar; }
			}

			/// <summary>
			/// Gets or sets flag to trim reductions.
			/// </summary>
			public bool TrimReductions
			{
				get { return m_trimReductions; }
				set { m_trimReductions = value; }
			}

			/// <summary>
			/// Gets a hash table of all parsed comment lines.
			/// </summary>
			/// <remarks>Line number is used as a key.</remarks>
			public Hashtable Comments
			{
				get { return m_comments; }
			}

			/// <summary>
			/// Gets current LALR state.
			/// </summary>
			public LalrState CurrentLalrState
			{
				get { return m_currentLalrState; }
			}

			/// <summary>
			/// Gets current reduction.
			/// </summary>
			public Reduction CurrentReduction
			{
				get
				{
					if (m_haveReduction)
					{
						return m_stack.PeekToken().Reduction;
					}
					else
					{
						return null;
					}
				}

				set
				{
					if (m_haveReduction)
					{
						m_stack.PeekToken().Reduction = value;
					}
				}
			}

			/// <summary>
			/// Gets current line.
			/// </summary>
			public int LineNumber
			{
				get { return m_tokenReader.LineNumber; }
			}

			/// <summary>
			/// Gets current token.
			/// </summary>
			public Token CurrentToken
			{
				get { return m_inputTokens.PeekToken(); }
			}

			/// <summary>
			/// Gets current token stack.
			/// </summary>
			public TokenStack TokenTable
			{
				get { return m_tokens; }
			}

			/// <summary>
			/// Executes a parse.  When this method is called, the parsing engine
			/// reads information from the source text (either a string or a file)
			/// and then reports what action was taken. This ranges from a token
			/// being read and recognized from the source, a parse reduction, or a type of error.
			/// </summary>
			/// <returns>ParseMessage indicating parser state.</returns>
			public ParseMessage Parse()
			{
				if (m_grammar == null)
				{
					return ParseMessage.NotLoadedError;
				}
				while (true)
				{
					Token readToken;
					if (m_inputTokens.Count == 0)
					{
						//We must read a token
						readToken = m_tokenReader.ReadToken();
						if (readToken == null)
						{
							return ParseMessage.InternalError;
						}
						else if (readToken.SymbolType != SymbolType.Whitespace)
						{
							m_inputTokens.Push(readToken);
							if (m_commentLevel == 0
								&& readToken.SymbolType != SymbolType.CommentLine
								&& readToken.SymbolType != SymbolType.CommentStart)
							{
								return ParseMessage.TokenRead;
							}
						}
					}
					else if (m_commentLevel > 0)
					{
						//We are in a block comment
						readToken = m_inputTokens.PopToken();
						if (readToken != null)
						{
							switch (readToken.SymbolType)
							{
								case SymbolType.CommentStart:
									m_commentLevel++;
									break;

								case SymbolType.CommentEnd:
									m_commentLevel--;
									break;
								case SymbolType.End:
									return ParseMessage.CommentError;
								default:
									//Do nothing, ignore
									//The 'comment line' symbol is ignored as well
									break;
							}
						}
					}
					else
					{
						readToken = m_inputTokens.PeekToken();
						if (readToken != null)
						{
							switch (readToken.SymbolType)
							{
								case SymbolType.CommentStart:
									m_commentLevel++;
									m_inputTokens.Pop(); //Remove it
									break;

								case SymbolType.CommentLine:
									m_inputTokens.Pop(); //Remove it and rest of line
									string comment = m_tokenReader.ReadToLineEnd();
									m_comments[LineNumber] = comment;
									break;

								case SymbolType.Error:
									return ParseMessage.LexicalError;

								default:
									//FINALLY, we can parse the token
									TokenParseResult parseResult = ParseToken(readToken);
									switch (parseResult)
									{
										case TokenParseResult.Accept:
											return ParseMessage.Accept;

										case TokenParseResult.InternalError:
											return ParseMessage.InternalError;

										case TokenParseResult.ReduceNormal:
											return ParseMessage.Reduction;

										case TokenParseResult.Shift:
											m_inputTokens.Pop(); //A simple shift, we must continue
											//Okay, remove the top token, it is on the stack
											break;

										case TokenParseResult.SyntaxError:
											return ParseMessage.SyntaxError;
									}
									break;
							}
						}
					}
				}
			}

			private TokenParseResult ParseToken(Token nextToken)
			{
				LalrStateAction stateAction = m_currentLalrState.GetActionBySymbolIndex(nextToken.Symbol.Index);
				m_tokens.Clear();

				if (stateAction != null)
				{
					//Work - shift or reduce
					m_haveReduction = false; //Will be set true if a reduction is made
					switch (stateAction.Action)
					{
						case LalrAction.Accept:
							m_haveReduction = true;
							return TokenParseResult.Accept;

						case LalrAction.Shift:
							m_currentLalrState = Grammar.LalrStateTable[stateAction.Value];
							nextToken.LalrState = m_currentLalrState;
							m_stack.Push(nextToken);
							return TokenParseResult.Shift;

						case LalrAction.Reduce:
							//Produce a reduction - remove as many tokens as members in the rule & push a nonterminal token
							int ruleIndex = stateAction.Value;
							Rule currentRule = Grammar.RuleTable[ruleIndex];

							//======== Create Reduction
							Token head;
							TokenParseResult parseResult;
							if (TrimReductions && currentRule.ContainsOneNonTerminal)
							{
								//The current rule only consists of a single nonterminal and can be trimmed from the
								//parse tree. Usually we create a new Reduction, assign it to the Data property
								//of Head and push it on the stack. However, in this case, the Data property of the
								//Head will be assigned the Data property of the reduced token (i.e. the only one
								//on the stack).
								//In this case, to save code, the value popped of the stack is changed into the head.
								head = m_stack.PopToken();
								head.Symbol = currentRule.NonTerminal;
								parseResult = TokenParseResult.ReduceEliminated;
							}
							else
							{
								//Build a Reduction
								m_haveReduction = true;
								Reduction reduction = new Reduction(currentRule);
								for (int i = 0; i < currentRule.Count; i++)
								{
									reduction.InsertToken(0, m_stack.PopToken());
								}
								head = new Token(currentRule.NonTerminal, reduction);
								parseResult = TokenParseResult.ReduceNormal;
							}

							//========== Goto
							LalrState nextState = m_stack.PeekToken().LalrState;

							//========= If nextAction is null here, then we have an Internal Table Error!!!!
							LalrStateAction nextAction = nextState.GetActionBySymbolIndex(currentRule.NonTerminal.Index);
							if (nextAction != null)
							{
								m_currentLalrState = Grammar.LalrStateTable[nextAction.Value];
								head.LalrState = m_currentLalrState;
								m_stack.Push(head);
								return parseResult;
							}
							else
							{
								return TokenParseResult.InternalError;
							}
					}
				}
				else
				{
					//=== Syntax Error! Fill Expected Tokens
					for (int i = 0; i < m_currentLalrState.ActionCount; i++)
					{
						switch (m_currentLalrState.GetAction(i).Symbol.SymbolType)
						{
							case SymbolType.Terminal:
							case SymbolType.End:
								Token token = new Token(m_currentLalrState.GetAction(i).Symbol, "", m_tokenReader.LineNumber);
								m_tokens.Push(token);
								break;
						}
					}
				}

				return TokenParseResult.SyntaxError;
			}

			/// <summary>
			/// Result of parsing token.
			/// </summary>
			private enum TokenParseResult
			{
				Empty = 0,
				Accept = 1,
				Shift = 2,
				ReduceNormal = 3,
				ReduceEliminated = 4,
				SyntaxError = 5,
				InternalError = 6
			}
		}
	}
}