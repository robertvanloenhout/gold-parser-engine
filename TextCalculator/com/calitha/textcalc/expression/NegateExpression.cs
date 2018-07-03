using System;

namespace com.calitha.textcalc.expression
{

	public class NegateExpression : UnaryExpression
	{
		public NegateExpression(Expression expression) :
			base(expression)
		{
		}

		public override int Evaluate(int value)
		{
			return -value;
		}

		public override double Evaluate(double value)
		{
			return -value;
		}

	}
}
