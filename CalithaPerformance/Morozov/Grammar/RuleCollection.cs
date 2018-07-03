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
		/// Contains read only collection of rules.
		/// </summary>
		public class RuleCollection : IEnumerable
		{
			private Rule[] m_ruleTable;

			/// <summary>
			/// Creates a new instance of <c>RuleCollection</c> class.
			/// </summary>
			/// <param name="ruleTable">
			/// Array of rules to initialize the collection.
			/// </param>
			public RuleCollection(Rule[] ruleTable)
			{
				m_ruleTable = ruleTable;
			}

			/// <summary>
			/// Gets a rule by its index.
			/// </summary>
			public Rule this[int index]
			{
				get { return m_ruleTable[index]; }
			}

			/// <summary>
			/// Gets number of rules.
			/// </summary>
			public int Count
			{
				get { return m_ruleTable.Length; }
			}

			#region IEnumerable Members

			/// <summary>
			/// Creates a new IEnumerator for the collection.
			/// </summary>
			/// <returns>New instance of IEnumerator.</returns>
			public IEnumerator GetEnumerator()
			{
				return m_ruleTable.GetEnumerator();
			}

			#endregion
		}
	}
}