using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.TokenParsers
{
    internal class BraceParser:ITokenParser
    {
        public bool GetLength(string code,int index,out int length)
        {
            length = 1;
            return
                code[index] == '(' || code[index] == ')' ||
                code[index] == '{' || code[index] == '}' ||
                code[index] == '[' || code[index] == ']';
        }
        public Token Parse(string code,int index)
        {
            switch (code[index])
            {
                case '(':
                    return new Token("BraceOpen", "(");
                case '{':
                    return new Token("BraceOpen", "{");
                case '[':
                    return new Token("BraceOpen", "[");

                case ')':
                    return new Token("BraceClose", ")");
                case '}':
                    return new Token("BraceClose", "}");
                case ']':
                    return new Token("BraceClose", "]");

                default:
                    throw new Exception();
            }
        }
    }
}
