
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swap.AST;
using Swap.Parser;

namespace Swap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sp = new Parser.SyntaxParser(new List<ITokenParser>() 
            {
                new Parser.TokenParsers.ArithmeticParser(),
                new Parser.TokenParsers.BraceParser(),
                new Parser.TokenParsers.IntParser(),
                new Parser.TokenParsers.SemiColonParser(),
                new Parser.TokenParsers.StringParser(),
                new Parser.TokenParsers.WordParser(),
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
