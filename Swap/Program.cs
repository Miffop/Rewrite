
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
            var sp = new Parser.Syntax.SyntaxParser(new List<ITokenParser>() 
            {
                new Parser.Syntax.TokenParsers.ArithmeticParser(),
                new Parser.Syntax.TokenParsers.BraceParser(),
                new Parser.Syntax.TokenParsers.IntParser(),
                new Parser.Syntax.TokenParsers.SemiColonParser(),
                new Parser.Syntax.TokenParsers.StringParser(),
                new Parser.Syntax.TokenParsers.WordParser(),
            });

            string path = "./../../Test.txt";
            string code = System.IO.File.ReadAllText(path);
            List<Token> tokens = sp.Parse(code);
            foreach(Token token in tokens)
            {
                Console.WriteLine(token.ToString());
            }
            Console.ReadKey();
        }
    }
}
