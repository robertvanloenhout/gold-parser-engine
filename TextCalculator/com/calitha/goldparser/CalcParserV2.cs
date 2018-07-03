using System;
using System.IO;
using System.Globalization;
using com.calitha.textcalc;
using com.calitha.textcalc.expression;
using com.calitha.commons;

namespace com.calitha.goldparser
{

	// Exceptions and constants are defined in CalcParserV1

	public class CalcParserV2 : IParser
	{
		private LALRParser parser;
		private NumberFormatInfo numberFormatInfo;

		public CalcParserV2(string filename)
		{
			FileStream stream = new FileStream(filename, 
				FileMode.Open, FileAccess.Read, FileShare.Read);
			Init(stream);
			stream.Close();
		}

		public CalcParserV2(string baseName, string resourceName)
		{
			byte[] buffer = ResourceUtil.GetByteArrayResource(
				System.Reflection.Assembly.GetExecutingAssembly(),
				baseName,
				resourceName);
			MemoryStream stream = new MemoryStream(buffer);
			Init(stream);
			stream.Close();
		}

		public CalcParserV2(Stream stream)
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

			parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
			parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
		}

		public void Parse(string source)
		{
			NonterminalToken token = parser.Parse(source);
			if (token != null)
			{
				MainForm.mainForm.ClearShowInputError();
				Expression exp = (Expression)CreateObject(token);
				MainForm.mainForm.WriteResult(exp.Evaluate().ToString());
			}
		}

		private Expression CreateExpression(Token token)
		{
			return (Expression)CreateObject(token);
		}

		private Object CreateObject(Token token)
		{
			if (token is TerminalToken)
				return CreateObjectFromTerminal((TerminalToken)token);
			else
				return CreateObjectFromNonterminal((NonterminalToken)token);
		}

		private Object CreateObjectFromTerminal(TerminalToken token)
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

		public Object CreateObjectFromNonterminal(NonterminalToken token)
		{
			switch (token.Rule.Id)
			{
				case (int)RuleConstants.RULE_EXPRESSION_PLUS :
					return new AddExpression(
						CreateExpression(token.Tokens[0]),
						CreateExpression(token.Tokens[2]));

				case (int)RuleConstants.RULE_EXPRESSION_MINUS :
					return new SubtractExpression(
						CreateExpression(token.Tokens[0]),
						CreateExpression(token.Tokens[2]));

				case (int)RuleConstants.RULE_EXPRESSION : 
					return CreateExpression(token.Tokens[0]);

				case (int)RuleConstants.RULE_MULTEXP_TIMES :
					return new MultiplyExpression(
						CreateExpression(token.Tokens[0]),
						CreateExpression(token.Tokens[2]));

				case (int)RuleConstants.RULE_MULTEXP_DIV :
					return new DivideExpression(
						CreateExpression(token.Tokens[0]),
						CreateExpression(token.Tokens[2]));

				case (int)RuleConstants.RULE_MULTEXP :
					return CreateExpression(token.Tokens[0]);

				case (int)RuleConstants.RULE_NEGATEEXP_MINUS :
					return new NegateExpression(
						CreateExpression(token.Tokens[1]));

				case (int)RuleConstants.RULE_NEGATEEXP :
					return CreateExpression(token.Tokens[0]);

				case (int)RuleConstants.RULE_VALUE_INTEGER :
					return CreateExpression(token.Tokens[0]);

				case (int)RuleConstants.RULE_VALUE_FLOAT :
					return CreateExpression(token.Tokens[0]);

				case (int)RuleConstants.RULE_VALUE_LPARAN_RPARAN :
					return CreateExpression(token.Tokens[1]);

			}
			throw new RuleException("Unknown rule");
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
