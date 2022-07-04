using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Syntax.TokenParsers
{
    internal class UStringParser:ITokenParser
    {
        public bool GetLength(string code,int index,out int length)
        {
            length = 2;
            if (code[index++] != '`')
            {
                return false;
            }
            while (code[index++] != '`')
            {
                length++;
            }
            return true;
        }
        public Token Parse(string code,int index)
        {
            string s = "";
            index++;
            while (code[index] != '`')
            {
                s += code[index++];
            }
            return new Token("UString", s.Replace("\\q", "\"").Replace("\\b", "\\"));
        }
    }
}
