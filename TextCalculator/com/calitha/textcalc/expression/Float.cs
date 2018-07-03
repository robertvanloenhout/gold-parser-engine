using System;

namespace com.calitha.textcalc.expression
{

	public class Float : Value
	{
		private static int level = 2;

		private double value;

		public Float(double value)
		{
			this.value = value;
		}

		public override int GetLevel()
		{
			return level;
		}

		public override Value Evaluate()
		{
			return this;
		}

		public override Value Evaluate(UnaryExpression op)
		{
			return ValueFactory.CreateValue(op.Evaluate(GetValue()));
		}

		public override Value Evaluate(Value right, BinaryExpression op)
		{
			if (this.GetLevel() >= right.GetLevel())
			{
				Float rightF = (Float)right.Convert(typeof (Float));
				Value result = ValueFactory.CreateValue(
					op.Evaluate(this.GetValue(), rightF.GetValue()));
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
				Float leftF = (Float)left.Convert(typeof (Float));
				Value result = ValueFactory.CreateValue(
					op.Evaluate(leftF.GetValue(), this.GetValue()));
				return result;
			} 
			else
			{
				return left.Evaluate(this, op);
			}
		}

		public double GetValue()
		{
			return value;
		}

		public override Value Convert(Type type)
		{
			if (type == typeof (Float))
				return this;
			else
				return new Integer((int)this.GetValue());
		}

		public override string ToString()
		{
			return ""+GetValue();
		}


	}
}
