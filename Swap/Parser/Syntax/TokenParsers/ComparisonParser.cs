using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Syntax.TokenParsers
{
    internal class ComparisonParser:ITokenParser
    {
        public bool GetLength(string code, int index,out int length)
        {
            if (code[index] == '=' && code[index + 1] == '=')
            {
                length = 2;
                return true;
            }
            else if (code[index] == '!' && code[index + 1] == '=')
            {
                length = 2;
                return true;
            }
            else if (code[index] == '>' || code[index] == '<')
            {
                if (code[index + 1] == '=')
                {
                    length = 2;
                }
                else
                {
                    length = 1;
                }
                return true;
            }
            length = 0;
            return false;
        }
        public Token Parse(string code,int index)
        {
            if(code[index]=='=' && code[index + 1] == '=')
            {
                return new Token("Oper", "==");
            }
            else if(code[index]=='!' && code[index + 1] == '=')
            {
                return new Token("Oper", "!=");
            }
            else if (code[index] == '>' || code[index] == '<')
            {
                if (code[index + 1] == '=')
                {
                    return new Token("Oper", code.Substring(index,2));
                }
                else
                {
                    return new Token("Oper", code.Substring(index, 1));
                }
            }
            throw new NotImplementedException();
        }
    }
}
