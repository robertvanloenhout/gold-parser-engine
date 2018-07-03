using System;
using System.IO;
using System.Runtime.Serialization;
using System.Globalization;
using com.calitha.goldparser.lalr;
using com.calitha.textcalc;
using com.calitha.textcalc.expression;
using com.calitha.commons;

namespace com.calitha.goldparser
{

	[Serializable()]
	public class SymbolException : System.Exception
	{
		public SymbolException(string message) : base(message)
		{
		}

		public SymbolException(string message,
			Exception inner) : base(message, inner)
		{
		}

		protected SymbolException(SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}

	}

	[Serializable()]
	public class RuleException : System.Exception
	{

		public RuleException(string message) : base(message)
		{
		}

		public RuleException(string message,
			Exception inner) : base(message, inner)
		{
		}

		protected RuleException(SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}

	}

	enum SymbolConstants : int
	{
		SYMBOL_EOF        = 0 , // (EOF)
		SYMBOL_ERROR      = 1 , // (Error)
		SYMBOL_WHITESPACE = 2 , // (Whitespace)
		SYMBOL_MINUS      = 3 , // '-'
		SYMBOL_LPARAN     = 4 , // '('
		SYMBOL_RPARAN     = 5 , // ')'
		SYMBOL_TIMES      = 6 , // '*'
		SYMBOL_DIV        = 7 , // '/'
		SYMBOL_PLUS       = 8 , // '+'
		SYMBOL_FLOAT      = 9 , // Float
		SYMBOL_INTEGER    = 10, // Integer
		SYMBOL_EXPRESSION = 11, // <Expression>
		SYMBOL_MULTEXP    = 12, // <Mult Exp>
		SYMBOL_NEGATEEXP  = 13, // <Negate Exp>
		SYMBOL_VALUE      = 14  // <Value>
	};

	enum RuleConstants : int
	{
		RULE_EXPRESSION_PLUS     = 0 , // <Expression> ::= <Expression> '+' <Mult Exp>
		RULE_EXPRESSION_MINUS    = 1 , // <Expression> ::= <Expression> '-' <Mult Exp>
		RULE_EXPRESSION          = 2 , // <Expression> ::= <Mult Exp>
		RULE_MULTEXP_TIMES       = 3 , // <Mult Exp> ::= <Mult Exp> '*' <Negate Exp>
		RULE_MULTEXP_DIV         = 4 , // <Mult Exp> ::= <Mult Exp> '/' <Negate Exp>
		RULE_MULTEXP             = 5 , // <Mult Exp> ::= <Negate Exp>
		RULE_NEGATEEXP_MINUS     = 6 , // <Negate Exp> ::= '-' <Value>
		RULE_NEGATEEXP           = 7 , // <Negate Exp> ::= <Value>
		RULE_VALUE_INTEGER       = 8 , // <Value> ::= Integer
		RULE_VALUE_FLOAT         = 9 , // <Value> ::= Float
		RULE_VALUE_LPARAN_RPARAN = 10  // <Value> ::= '(' <Expression> ')'
	};

	public class CalcParserV1 : IParser
	{
		private LALRParser parser;
		private NumberFormatInfo numberFormatInfo;

		public CalcParserV1(string filename)
		{
			FileStream stream = new FileStream(filename, 
				FileMode.Open, FileAccess.Read, FileShare.Read);
			Init(stream);
			stream.Close();
		}

		public CalcParserV1(string baseName, string resourceName)
		{
			byte[] buffer = ResourceUtil.GetByteArrayResource(
				System.Reflection.Assembly.GetExecutingAssembly(),
				baseName,
				resourceName);
			MemoryStream stream = new MemoryStream(buffer);
			Init(stream);
			stream.Close();
		}

		public CalcParserV1(Stream stream)
		{
			Init(stream);
		}

		private void Init(Stream stream)
		{
		    numberFormatInfo = new NumberFormatInfo();
		    numberFormatInfo.NumberDecimalSeparator = ".";
			CGTReader reader = new CGTReader(stream);
			parser = reader.CreateNewParser();
			parser.TrimReductions = false;
			parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

			parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
			parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
			parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
			parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
			parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
		}

		public void Parse(string source)
		{
			parser.Parse(source);

		}

		private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
		{
			try
			{
				args.Token.UserObject = CreateObject(args.Token);
			}
			catch (Exception e)
			{
				args.Continue = false;
				MainForm.mainForm.WriteResult(e.Message);
			}
		}

		private Object CreateObject(TerminalToken token)
		{
			switch (token.Symbol.Id)
			{
				case (int)SymbolConstants.SYMBOL_EOF :
					return null;
				case (int)SymbolConstants.SYMBOL_WHITESPACE :
					return null;
				case (int)SymbolConstants.SYMBOL_MINUS :
					return null;
				case (int)SymbolConstants.SYMBOL_LPARAN :
					return null;
				case (int)SymbolConstants.SYMBOL_RPARAN :
					return null;
				case (int)SymbolConstants.SYMBOL_TIMES :
					return null;
				case (int)SymbolConstants.SYMBOL_DIV :
					return null;
				case (int)SymbolConstants.SYMBOL_PLUS :
					return null;
				case (int)SymbolConstants.SYMBOL_FLOAT :
					return ValueFactory.CreateValue(Double.Parse(token.Text,
					                                             numberFormatInfo));
				case (int)SymbolConstants.SYMBOL_INTEGER :
					return ValueFactory.CreateValue(Int32.Parse(token.Text));
			}
			throw new SymbolException("Unknown symbol");
		}

		private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
		{
			try
			{
				args.Token.UserObject = CreateObject(args.Token);
			}
			catch (Exception e)
			{
				args.Continue = false;
				MainForm.mainForm.WriteResult(e.Message);
			}
		}

		public static Object CreateObject(NonterminalToken token)
		{
			switch (token.Rule.Id)
			{
				case (int)RuleConstants.RULE_EXPRESSION_PLUS :
					return new AddExpression(
						(Expression)token.Tokens[0].UserObject,
						(Expression)token.Tokens[2].UserObject);

				case (int)RuleConstants.RULE_EXPRESSION_MINUS :
					return new SubtractExpression(
						(Expression)token.Tokens[0].UserObject,
						(Expression)token.Tokens[2].UserObject);

				case (int)RuleConstants.RULE_EXPRESSION : 
					return (Expression)token.Tokens[0].UserObject;

				case (int)RuleConstants.RULE_MULTEXP_TIMES :
					return new MultiplyExpression(
						(Expression)token.Tokens[0].UserObject,
						(Expression)token.Tokens[2].UserObject);

				case (int)RuleConstants.RULE_MULTEXP_DIV :
					return new DivideExpression(
						(Expression)token.Tokens[0].UserObject,
						(Expression)token.Tokens[2].UserObject);

				case (int)RuleConstants.RULE_MULTEXP :
					return (Expression)token.Tokens[0].UserObject;

				case (int)RuleConstants.RULE_NEGATEEXP_MINUS :
					return new NegateExpression(
						(Expression)token.Tokens[1].UserObject);

				case (int)RuleConstants.RULE_NEGATEEXP :
					return (Expression)token.Tokens[0].UserObject;

				case (int)RuleConstants.RULE_VALUE_INTEGER :
					return (Expression)token.Tokens[0].UserObject;

				case (int)RuleConstants.RULE_VALUE_FLOAT :
					return (Expression)token.Tokens[0].UserObject;

				case (int)RuleConstants.RULE_VALUE_LPARAN_RPARAN :
					return (Expression)token.Tokens[1].UserObject;

			}
			throw new RuleException("Unknown rule");
		}


		private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
		{
			MainForm.mainForm.ClearShowInputError();
			try
			{
				Expression exp = (Expression)args.Token.UserObject;
				MainForm.mainForm.WriteResult(exp.Evaluate().ToString());
			}
			catch (Exception e)
			{
				MainForm.mainForm.WriteResult(e.Message);
			}
		}

		private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
		{
			MainForm.mainForm.ShowInputError(args.Token);
			MainForm.mainForm.WriteResult("Token error with input: '"+args.Token.ToString()+"'");
		}

		private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
		{
			MainForm.mainForm.ShowInputError(args.UnexpectedToken);
			MainForm.mainForm.WriteResult("Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'");
		}


	}
	
}
