
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swap.AST;
using Swap.Parser.Syntax;

namespace Swap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Token> tokens;
            {
                var sp = new Parser.Syntax.SyntaxParser(new List<ITokenParser>()
                {
                new Parser.Syntax.TokenParsers.ArithmeticParser(),
                new Parser.Syntax.TokenParsers.BraceParser(),
                new Parser.Syntax.TokenParsers.IntParser(),
                new Parser.Syntax.TokenParsers.SemiColonParser(),
                new Parser.Syntax.TokenParsers.StringParser(),
                new Parser.Syntax.TokenParsers.WordParser(),
                });
                string code = "((this))";
                tokens = sp.Parse(code);
            }
            IExpression result;
            {
                var ep = new Parser.Expressions.ExpressionParser(
                    new List<Parser.Expressions.IOperationParser>()
                    {

                    },
                    new List<Parser.Expressions.IValueParser>()
                    {
                        new Parser.Expressions.ValueParsers.IntAndStringParser(),
                        new Parser.Expressions.ValueParsers.NodeNamesParser(),
                    }
                );
                result = ep.Parse(tokens, 0, tokens.Count);
            }
            Console.WriteLine(result.Stringify());

            Console.ReadKey();
        }
    }
}
