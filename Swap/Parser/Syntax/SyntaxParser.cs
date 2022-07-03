using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Syntax
{
    internal class SyntaxParser
    {
        List<ITokenParser> Parser;
        public SyntaxParser(List<ITokenParser> parsers)
        {
            this.Parser = parsers;
        }
        public List<Token> Parse(string code)
        {
            List<Token> result = new List<Token>();
            int index = 0;
            while (index<code.Length)
            {
                int bestLength = 0;
                ITokenParser bestParser = null;
                foreach(ITokenParser p in Parser)
                {
                    int currentLength;
                    if (p.GetLength(code,index,out currentLength) && currentLength>bestLength)
                    {
                        bestParser = p;
                        bestLength = currentLength;
                    }
                }
                if (bestParser != null)
                {
                    result.Add(bestParser.Parse(code, index));
                    index += bestLength;
                }
                else
                {
                    index++;
                }

            }
            return result;

        }
    }
}
