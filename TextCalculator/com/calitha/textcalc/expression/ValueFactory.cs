using System;

namespace com.calitha.textcalc.expression
{
	public sealed class ValueFactory
	{
		private ValueFactory()
		{}

		public static Value CreateValue(int value)
		{
			return new Integer(value);
		}

		public static Value CreateValue(double value)
		{
			return new Float(value);
		}


	}
}
