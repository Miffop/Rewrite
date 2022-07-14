using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Syntax.TokenParsers
{
    internal class IntParser:ITokenParser
    {
        public bool GetLength(string code,int index,out int length)
        {
            length = 0;
            while (Char.IsDigit(code[index++]))
            {
                length++;
            }
            return length > 0;
        }
        public Token Parse(string code,int index)
        {
            string num = "";
            while (Char.IsDigit(code[index]))
            {
                num+=code[index++];
            }
            return new Token("Int", num);
        }
    }
}
