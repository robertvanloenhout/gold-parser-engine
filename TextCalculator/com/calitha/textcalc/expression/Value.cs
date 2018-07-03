using System;
using System.CodeDom;

namespace com.calitha.textcalc.expression
{

	public abstract class Value : Expression
	{
		public abstract Value Evaluate(UnaryExpression op);
		public abstract Value Evaluate(Value other, BinaryExpression op);
		public abstract Value EvaluateReverse(Value left, BinaryExpression op);

		public abstract Value Convert(Type type);

		public abstract int GetLevel();

	}

}
