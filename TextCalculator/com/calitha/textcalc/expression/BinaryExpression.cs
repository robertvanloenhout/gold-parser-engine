using System;

namespace com.calitha.textcalc.expression
{

	public abstract class BinaryExpression : Expression
	{
		private Expression left;
		private Expression right;

		public BinaryExpression(Expression left, Expression right)
		{
			this.left = left;
			this.right = right;
		}

		public virtual Expression GetLeft()
		{
			return left;
		}

		public virtual Expression GetRight()
		{
			return right;
		}

		public override Value Evaluate()
		{
			Value lvalue = GetLeft().Evaluate();
			Value rvalue = GetRight().Evaluate();
			return lvalue.Evaluate(rvalue, this);
		}

		public abstract int Evaluate(int left, int right);
		public abstract double Evaluate(double left, double right);

	}
}
