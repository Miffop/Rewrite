using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Syntax
{
    internal interface ITokenParser
    {
        bool GetLength(string code,int index,out int length);
        Token Parse(string code, int index);
    }
}
