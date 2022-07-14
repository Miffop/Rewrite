using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Expressions.ValueParsers
{
    internal class NullParser:IValueParser
    {
        public bool Parse(List<Syntax.Token> code,int index,int length, Expressions.ExpressionParser ep, out AST.IExpression val)
        {
            if(length==1 && code[index].Command=="Word" && code[index].Argument == "null")
            {
                val = new AST.Expressions.ValueExpression(new AST.Values.VNull(), null);
                return true;
            }
            val = null;
            return false;
        }
    }
}
