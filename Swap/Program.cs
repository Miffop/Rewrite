
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
                    new Parser.Syntax.TokenParsers.ComparisonParser(),
                });
                string code = System.IO.File.ReadAllText("./../../zProg/Test.txt");
                //string code = "4*2*this";
                tokens = sp.Parse(code);
            }
            Parser.Expressions.ExpressionParser ep;
            {
                ep = new Parser.Expressions.ExpressionParser(
                    new List<Parser.Expressions.IOperationParser>()
                    {
                        new Parser.Expressions.OperationParsers.MathOperationParser(),
                        new Parser.Expressions.OperationParsers.ReflectionOperationParser(),
                        new Parser.Expressions.OperationParsers.ComparisonOperationParser(),
                    },
                    new List<Parser.Expressions.IValueParser>()
                    {
                        new Parser.Expressions.ValueParsers.IntAndStringParser(),
                        new Parser.Expressions.ValueParsers.NodeNamesParser(),
                        new Parser.Expressions.ValueParsers.NullParser(),
                        new Parser.Expressions.ValueParsers.UnaryFunctionParser(),
                    },
                    true
                );
            }
            {
                var cp = new Parser.Commands.CommandParser(new List<Parser.Commands.ICommandParser>()
                {
                    new Parser.Commands.CommandParsers.UnaryCommandParser(),
                    new Parser.Commands.CommandParsers.ReflectionCommandParser(),
                    new Parser.Commands.CommandParsers.EmptyCommandParser(),
                });
                var com = cp.Parse(tokens, 0, tokens.Count, 1, ep);
                
                Console.WriteLine(com.Stringify());
                
                LinkedListNode<ICommand> rootNode = new LinkedListNode<ICommand>(com);
                com.Parent = rootNode;
                com.Execute(new Context(com, com));
                

            }
            //Console.WriteLine(result.Eval(null).Stringify());

            Console.ReadKey();
        }
    }
}
