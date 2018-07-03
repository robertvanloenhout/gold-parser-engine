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
		/// LALR action type.
		/// </summary>
		public enum LalrAction
		{
			/// <summary>
			/// Shift a symbol and goto a state
			/// </summary>
			Shift = 1,

			/// <summary>
			/// Reduce by a specified rule
			/// </summary>
			Reduce = 2,

			/// <summary>
			/// Goto to a state on reduction
			/// </summary>
			Goto = 3,

			/// <summary>
			/// Input successfully parsed
			/// </summary>
			Accept = 4,

			/// <summary>
			/// Error
			/// </summary>
			Error = 5
		}
	}
}