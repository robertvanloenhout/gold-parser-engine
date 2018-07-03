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
		/// Stack of tokens.
		/// </summary>
		public class TokenStack : Stack
		{
			/// <summary>
			/// Creates a new instance of the <c>TokenStack</c> class.
			/// </summary>
			public TokenStack()
				: base()
			{
			}

			/// <summary>
			/// Pushes token to the stack.
			/// </summary>
			/// <param name="token">Token to push to the stack.</param>
			public void PushToken(Token token)
			{
				Push(token);
			}

			/// <summary>
			/// Pops token from the stack.
			/// </summary>
			/// <returns>Token from the stack.</returns>
			public Token PopToken()
			{
				return (Token)Pop();
			}

			/// <summary>
			/// Gets token from the top of the stack.
			/// </summary>
			/// <returns>Token from the top of the stack.</returns>
			public Token PeekToken()
			{
				return (Token)Peek();
			}

			/// <summary>
			/// Copies the token stack contents to the token array.
			/// </summary>
			/// <param name="array">Array to copy data to.</param>
			/// <param name="index">Start index in the array to copy tokens.</param>
			public void CopyTo(Token[] array, int index)
			{
				foreach (Token token in this)
				{
					array[index++] = token;
				}
			}
		}
	}
}