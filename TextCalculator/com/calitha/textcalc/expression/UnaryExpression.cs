using System;

namespace com.calitha.textcalc.expression
{

	public abstract class UnaryExpression : Expression
	{
		private Expression expression;

		public UnaryExpression(Expression expression)
		{
			this.expression = expression;
		}

		public Expression GetExpression()
		{
			return expression;
		}

		public override Value Evaluate()
		{
			Value value = GetExpression().Evaluate();
			return value.Evaluate(this);
		}

		public abstract int Evaluate(int value);
		public abstract double Evaluate(double value);

	}
}
