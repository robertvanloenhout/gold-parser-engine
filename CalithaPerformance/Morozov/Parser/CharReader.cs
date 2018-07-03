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
		/// Reads characters from a string.
		/// </summary>
		public class CharReader
		{
			private string m_source;
			private int m_index;
			private int m_lineNumber = 1;

			/// <summary>
			/// Special character to identify end of the string.
			/// </summary>
			public const char EndOfString = (char)0;

			/// <summary>
			/// Creates a new instance of <c>CharReader</c> class.
			/// </summary>
			/// <param name="source">Source string to parse.</param>
			public CharReader(string source)
			{
				m_source = source;
				m_index = 0;
			}

			/// <summary>
			/// Gets current char.
			/// </summary>
			public char CurrentChar
			{
				get { return GetChar(0); }
			}

			/// <summary>
			/// Gets a look-ahead char.
			/// </summary>
			/// <param name="shift">Number of chars to look ahead.</param>
			/// <returns>Char at requested position.</returns>
			public char GetChar(int shift)
			{
				int index = m_index + shift;
				if ((index >= 0) && (index < m_source.Length))
				{
					return m_source[index];
				}
				return EndOfString;
			}

			/// <summary>
			/// Returns current char and movesto the next char.
			/// </summary>
			/// <returns>Current char.</returns>
			public string Accept()
			{
				int start = m_index;
				if (m_index < m_source.Length)
				{
					if (m_source[m_index] == '\n')
					{
						m_lineNumber++;
					}
					m_index++;
				}
				int count = m_index - start;
				if (count > 0)
				{
					return m_source.Substring(start, count);
				}
				return String.Empty;
			}

			/// <summary>
			/// Returns a string and moves the pointer to the next char after it.
			/// </summary>
			/// <param name="count">Number of chars to read.</param>
			/// <returns>String with count chars.</returns>
			public string Accept(int count)
			{
				int start = m_index;
				if (count < 0)
				{
					count = 0;
				}
				else if (start + count >= m_source.Length)
				{
					count = m_source.Length - start;
				}
				for (int i = 0; i < count; i++)
				{
					if (m_source[m_index] == '\n')
					{
						m_lineNumber++;
					}
					m_index++;
				}
				if (count > 0)
				{
					return m_source.Substring(start, count);
				}
				return String.Empty;
			}

			/// <summary>
			/// Returns current char and increments current index.
			/// </summary>
			/// <returns>Current char.</returns>
			public char AcceptChar()
			{
				char result = CurrentChar;
				if (m_index < m_source.Length)
				{
					if (result == '\n')
					{
						m_lineNumber++;
					}
					m_index++;
				}
				return result;
			}

			/// <summary>
			/// Returns substring till the end of line and moves
			/// current index to position after it.
			/// </summary>
			/// <returns>Substring till the end of line.</returns>
			public string AcceptToLineEnd()
			{
				int start = m_index;
				while ((CurrentChar != '\n')
					&& (CurrentChar != '\r')
					&& (CurrentChar != EndOfString))
				{
					AcceptChar();
				}
				return m_source.Substring(start, m_index - start);
			}

			/// <summary>
			/// Gets current line number. It is 1-based.
			/// </summary>
			public int LineNumber
			{
				get { return m_lineNumber; }
			}
		}
	}
}