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
using System.IO;
using System.Text;
using System.Collections;

namespace Morozov
{
	namespace GoldParser
	{
		/// <summary>
		/// Contains grammar tables required for parsing.
		/// </summary>
		public class Grammar
		{
			#region Public constants

			/// <summary>
			/// Identifies Gold parser grammar file.
			/// </summary>
			public const string FileHeader = "GOLD Parser Tables/v1.0";

			#endregion

			#region Private field declaration

			// Grammar header information
			private string m_name;                // Name of the grammar
			private string m_version;             // Version of the grammar
			private string m_author;              // Author of the grammar
			private string m_about;               // Grammar description
			private int m_startSymbolIndex;    // Start symbol index
			private bool m_caseSensitive;       // Grammar is case sensitive or not

			// Tables read from the binary grammar file
			private Symbol[] m_symbolTable;    // Symbol table
			private String[] m_charSetTable;   // Charset table
			private Rule[] m_ruleTable;      // Rule table
			private DfaState[] m_dfaStateTable;  // DFA state table
			private LalrState[] m_lalrStateTable; // LALR state table

			// Tables exposed as public properties 
			private SymbolCollection m_symbols;    // Symbol table 
			private CharSetCollection m_charSets;   // Charset table
			private RuleCollection m_rules;      // Rule table
			private DfaStateCollection m_dfaStates;  // DFA state table
			private LalrStateCollection m_lalrStates; // LALR state table

			// Initial states
			private int m_dfaInitialState;        // DFA initial state
			private int m_lalrInitialState;       // LALR initial state

			// Internal state of grammar parser
			private BinaryReader m_reader;        // Source of the grammar    
			private int m_entryCount;             // Number of entries left

			#endregion

			#region Constructors

			/// <summary>
			/// Creates a new instance of <c>Grammar</c> class
			/// </summary>
			/// <param name="reader"></param>
			public Grammar(BinaryReader reader)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}

				m_reader = reader;
				Load();
			}

			#endregion

			#region Public members

			/// <summary>
			/// Gets grammar name.
			/// </summary>
			public string Name
			{
				get { return m_name; }
			}

			/// <summary>
			/// Gets grammar version.
			/// </summary>
			public string Version
			{
				get { return m_version; }
			}

			/// <summary>
			/// Gets grammar author.
			/// </summary>
			public string Author
			{
				get { return m_author; }
			}

			/// <summary>
			/// Gets grammar description.
			/// </summary>
			public string About
			{
				get { return m_about; }
			}

			/// <summary>
			/// Gets the start symbol for the grammar.
			/// </summary>
			public Symbol StartSymbol
			{
				get { return m_symbolTable[m_startSymbolIndex]; }
			}

			/// <summary>
			/// Gets the value indicating if the grammar is case sensitive.
			/// </summary>
			public bool CaseSensitive
			{
				get { return m_caseSensitive; }
			}

			/// <summary>
			/// Gets initial DFA state.
			/// </summary>
			public int DfaInitialState
			{
				get { return m_dfaInitialState; }
			}

			/// <summary>
			/// Gets initial LALR state.
			/// </summary>
			public LalrState InitialLalrState
			{
				get { return m_lalrStateTable[m_lalrInitialState]; }
			}

			/// <summary>
			/// Gets symbol table.
			/// </summary>
			public SymbolCollection SymbolTable
			{
				get
				{
					if (m_symbols == null)
					{
						m_symbols = new SymbolCollection(m_symbolTable);
					}
					return m_symbols;
				}
			}

			/// <summary>
			/// Gets char set table.
			/// </summary>
			public CharSetCollection CharSetTable
			{
				get
				{
					if (m_charSets == null)
					{
						m_charSets = new CharSetCollection(m_charSetTable);
					}
					return m_charSets;
				}
			}

			/// <summary>
			/// Gets rule table.
			/// </summary>
			public RuleCollection RuleTable
			{
				get
				{
					if (m_rules == null)
					{
						m_rules = new RuleCollection(m_ruleTable);
					}
					return m_rules;
				}
			}

			/// <summary>
			/// Gets DFA state table.
			/// </summary>
			public DfaStateCollection DfaStateTable
			{
				get
				{
					if (m_dfaStates == null)
					{
						m_dfaStates = new DfaStateCollection(m_dfaStateTable);
					}
					return m_dfaStates;
				}
			}

			/// <summary>
			/// Gets LALR state table.
			/// </summary>
			public LalrStateCollection LalrStateTable
			{
				get
				{
					if (m_lalrStates == null)
					{
						m_lalrStates = new LalrStateCollection(m_lalrStateTable);
					}
					return m_lalrStates;
				}
			}

			#endregion

			#region Private members

			/// <summary>
			/// Loads grammar from the binary reader.
			/// </summary>
			private void Load()
			{
				if (FileHeader != ReadString())
				{
					throw new Exception(Res.GetString(Res.Grammar_WrongFileHeader));
				}
				while (m_reader.PeekChar() != -1)
				{
					RecordType recordType = ReadNextRecord();
					switch (recordType)
					{
						case RecordType.Parameters:
							ReadHeader();
							break;

						case RecordType.TableCounts:
							ReadTableCounts();
							break;

						case RecordType.Initial:
							ReadInitialStates();
							break;

						case RecordType.Symbols:
							ReadSymbols();
							break;

						case RecordType.CharSets:
							ReadCharSets();
							break;

						case RecordType.Rules:
							ReadRules();
							break;

						case RecordType.DfaStates:
							ReadDfaStates();
							break;

						case RecordType.LalrStates:
							ReadLalrStates();
							break;

						default:
							throw new Exception(Res.GetString(Res.Grammar_InvalidRecordType));
					}
				}
			}

			/// <summary>
			/// Reads the next record in the binary grammar file.
			/// </summary>
			/// <returns>Read record type.</returns>
			private RecordType ReadNextRecord()
			{
				char recordType = (char)ReadByte();
				//Structure below is ready for future expansion
				switch (recordType)
				{
					case 'M':
						//Read the number of entry's
						m_entryCount = ReadInt16();
						return (RecordType)ReadByteEntry();

					default:
						throw new Exception(Res.GetString(Res.Grammar_InvalidRecordHeader));
				}
			}

			/// <summary>
			/// Reads grammar header information.
			/// </summary>
			private void ReadHeader()
			{
				m_name = ReadStringEntry();
				m_version = ReadStringEntry();
				m_author = ReadStringEntry();
				m_about = ReadStringEntry();
				m_caseSensitive = ReadBoolEntry();
				m_startSymbolIndex = ReadInt16Entry();
			}

			/// <summary>
			/// Reads table record counts and initializes tables.
			/// </summary>
			private void ReadTableCounts()
			{
				// Initialize tables
				m_symbolTable = new Symbol[ReadInt16Entry()];
				m_charSetTable = new String[ReadInt16Entry()];
				m_ruleTable = new Rule[ReadInt16Entry()];
				m_dfaStateTable = new DfaState[ReadInt16Entry()];
				m_lalrStateTable = new LalrState[ReadInt16Entry()];
			}

			/// <summary>
			/// Read initial DFA and LALR states.
			/// </summary>
			private void ReadInitialStates()
			{
				m_dfaInitialState = ReadInt16Entry();
				m_lalrInitialState = ReadInt16Entry();
			}

			/// <summary>
			/// Read symbol information.
			/// </summary>
			private void ReadSymbols()
			{
				int index = ReadInt16Entry();
				string name = ReadStringEntry();
				SymbolType symbolType = (SymbolType)ReadInt16Entry();

				Symbol symbol = new Symbol(index, name, symbolType);
				m_symbolTable[index] = symbol;
			}

			/// <summary>
			/// Read char set information.
			/// </summary>
			private void ReadCharSets()
			{
				m_charSetTable[ReadInt16Entry()] = ReadStringEntry();
			}

			/// <summary>
			/// Read rule information.
			/// </summary>
			private void ReadRules()
			{
				int index = ReadInt16Entry();
				Symbol nonTerminal = m_symbolTable[ReadInt16Entry()];
				ReadEmptyEntry();
				Symbol[] symbols = new Symbol[m_entryCount];
				for (int i = 0; i < symbols.Length; i++)
				{
					symbols[i] = m_symbolTable[ReadInt16Entry()];
				}
				Rule rule = new Rule(index, nonTerminal, symbols);
				m_ruleTable[index] = rule;
			}

			/// <summary>
			/// Read DFA state information.
			/// </summary>
			private void ReadDfaStates()
			{
				int index = ReadInt16Entry();
				Symbol acceptSymbol = null;
				bool acceptState = ReadBoolEntry();
				if (acceptState)
				{
					acceptSymbol = m_symbolTable[ReadInt16Entry()];
				}
				else
				{
					ReadInt16Entry();  // Skip the entry.
				}
				ReadEmptyEntry();

				// Read DFA edges
				DfaEdge[] edges = new DfaEdge[m_entryCount / 3];
				for (int i = 0; i < edges.Length; i++)
				{
					edges[i].CharSetIndex = ReadInt16Entry();
					edges[i].TargetIndex = ReadInt16Entry();
					ReadEmptyEntry();
				}

				// Create DFA state and store it in DFA state table
				Hashtable transitionVector = CreateDfaTransitionVector(edges);
				DfaState dfaState = new DfaState(index, acceptSymbol, transitionVector);
				m_dfaStateTable[index] = dfaState;
			}

			/// <summary>
			/// Read LALR state information.
			/// </summary>
			private void ReadLalrStates()
			{
				int index = ReadInt16Entry();
				ReadEmptyEntry();
				LalrStateAction[] stateTable = new LalrStateAction[m_entryCount / 4];
				for (int i = 0; i < stateTable.Length; i++)
				{
					Symbol symbol = m_symbolTable[ReadInt16Entry()];
					LalrAction action = (LalrAction)ReadInt16Entry();
					int targetIndex = ReadInt16Entry();
					ReadEmptyEntry();
					stateTable[i] = new LalrStateAction(i, symbol, action, targetIndex);
				}

				// Create the transition vector
				LalrStateAction[] transitionVector = new LalrStateAction[m_symbolTable.Length];
				for (int i = 0; i < transitionVector.Length; i++)
				{
					transitionVector[i] = null;
				}
				for (int i = 0; i < stateTable.Length; i++)
				{
					transitionVector[stateTable[i].Symbol.Index] = stateTable[i];
				}

				LalrState lalrState = new LalrState(index, stateTable, transitionVector);
				m_lalrStateTable[index] = lalrState;
			}

			/// <summary>
			/// Creates the DFA state transition vector.
			/// </summary>
			/// <param name="edges">Array of automata edges.</param>
			/// <returns>Hashtable with the transition information.</returns>
			private Hashtable CreateDfaTransitionVector(DfaEdge[] edges)
			{
				Hashtable transitionVector = new Hashtable();
				//
				//89 is the initial prime number for the hash table size. 
				//It should be a good start for the most char sets.
				//
				for (int i = edges.Length; --i >= 0; )
				{
					string charSet = m_charSetTable[edges[i].CharSetIndex];
					for (int j = 0; j < charSet.Length; j++)
					{
						transitionVector[charSet[j]] = edges[i].TargetIndex;
					}
				}
				return transitionVector;
			}

			/// <summary>
			/// Reads empty entry from the grammar file.
			/// </summary>
			private void ReadEmptyEntry()
			{
				if (ReadEntryType() != EntryType.Empty)
				{
					throw new Exception(Res.GetString(Res.Grammar_EmptyEntryExpected));
				}
				m_entryCount--;
			}

			/// <summary>
			/// Reads string entry from the grammar file.
			/// </summary>
			/// <returns>String entry content.</returns>
			private string ReadStringEntry()
			{
				if (ReadEntryType() != EntryType.String)
				{
					throw new Exception(Res.GetString(Res.Grammar_StringEntryExpected));
				}
				m_entryCount--;
				return ReadString();
			}

			/// <summary>
			/// Reads Int16 entry from the grammar file.
			/// </summary>
			/// <returns>Int16 entry content.</returns>
			private int ReadInt16Entry()
			{
				if (ReadEntryType() != EntryType.Integer)
				{
					throw new Exception(Res.GetString(Res.Grammar_IntegerEntryExpected));
				}
				m_entryCount--;
				return ReadInt16();
			}

			/// <summary>
			/// Reads byte entry from the grammar file.
			/// </summary>
			/// <returns>Byte entry content.</returns>
			private byte ReadByteEntry()
			{
				if (ReadEntryType() != EntryType.Byte)
				{
					throw new Exception(Res.GetString(Res.Grammar_ByteEntryExpected));
				}
				m_entryCount--;
				return ReadByte();
			}

			/// <summary>
			/// Reads boolean entry from the grammar file.
			/// </summary>
			/// <returns>Boolean entry content.</returns>
			private bool ReadBoolEntry()
			{
				if (ReadEntryType() != EntryType.Boolean)
				{
					throw new Exception(Res.GetString(Res.Grammar_BooleanEntryExpected));
				}
				m_entryCount--;
				return ReadBool();
			}

			/// <summary>
			/// Reads entry type.
			/// </summary>
			/// <returns>Entry type.</returns>
			private EntryType ReadEntryType()
			{
				if (m_entryCount == 0)
				{
					throw new Exception(Res.GetString(Res.Grammar_NoEntry));
				}
				return (EntryType)ReadByte();
			}

			/// <summary>
			/// Reads string from the grammar file.
			/// </summary>
			/// <returns>String value.</returns>
			private string ReadString()
			{
				StringBuilder result = new StringBuilder();
				char unicodeChar = (char)ReadInt16();
				while (unicodeChar != (char)0)
				{
					result.Append(unicodeChar);
					unicodeChar = (char)ReadInt16();
				}
				return result.ToString();
			}

			/// <summary>
			/// Reads two byte integer Int16 from the grammar file.
			/// </summary>
			/// <returns>Int16 value.</returns>
			private int ReadInt16()
			{
				return m_reader.ReadUInt16();
			}

			/// <summary>
			/// Reads byte from the grammar file.
			/// </summary>
			/// <returns>Byte value.</returns>
			private byte ReadByte()
			{
				return m_reader.ReadByte();
			}

			/// <summary>
			/// Reads boolean from the grammar file.
			/// </summary>
			/// <returns>Boolean value.</returns>
			private bool ReadBool()
			{
				return (ReadByte() == 1);
			}

			#endregion

			#region Private type definitions

			/// <summary>
			/// Record type byte in the binary grammar file.
			/// </summary>
			private enum RecordType
			{
				Parameters = (int)'P', // 80
				TableCounts = (int)'T', // 84
				Initial = (int)'I', // 73
				Symbols = (int)'S', // 83
				CharSets = (int)'C', // 67
				Rules = (int)'R', // 82
				DfaStates = (int)'D', // 68
				LalrStates = (int)'L', // 76
				Comment = (int)'!'  // 33
			}

			/// <summary>
			/// Entry type byte in the binary grammar file.
			/// </summary>
			private enum EntryType
			{
				Empty = (int)'E', // 69
				Integer = (int)'I', // 73
				String = (int)'S', // 83
				Boolean = (int)'B', // 66
				Byte = (int)'b'  // 98
			}

			/// <summary>
			/// Edge between DFA states.
			/// </summary>
			private struct DfaEdge
			{
				public int CharSetIndex;
				public int TargetIndex;
			}

			#endregion
		}
	}
}