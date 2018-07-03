using System;

namespace com.calitha.textcalc.expression
{

	public class Integer : Value
	{
		private int value;
		private static int level = 1;

		public Integer(int value)
		{
			this.value = value;
		}

		public override Value Evaluate()
		{
			return this;
		}

		public override int GetLevel()
		{
			return level;
		}

		public int GetValue()
		{
			return value;
		}

		public override Value Evaluate(UnaryExpression op)
		{
			return ValueFactory.CreateValue(op.Evaluate(GetValue()));
		}

		public override Value Evaluate(Value right, BinaryExpression op)
		{
			if (this.GetLevel() >= right.GetLevel())
			{
				Integer rightI = (Integer)right.Convert(typeof (Integer));
				Value result = ValueFactory.CreateValue(
					op.Evaluate(this.GetValue(), rightI.GetValue()));
					return result;
			} 
			else
			{
				return right.EvaluateReverse(this, op);
			}
		}

		public override Value EvaluateReverse(Value left, BinaryExpression op)
		{
			if (this.GetLevel() >= left.GetLevel())
			{
				Integer leftI = (Integer)left.Convert(typeof (Integer));
				Value result = ValueFactory.CreateValue(
					op.Evaluate(leftI.GetValue(), this.GetValue()));
				return result;
			} 
			else
			{
				return left.Evaluate(this, op);
			}
		}

		public override Value Convert(Type type)
		{
			if (type == typeof(Integer))
				return this;
			else if (type == typeof(Float))
				return new Float(this.GetValue());
			else
				return null;
		}

		public override string ToString()
		{
			return ""+GetValue();
		}

	}
}
