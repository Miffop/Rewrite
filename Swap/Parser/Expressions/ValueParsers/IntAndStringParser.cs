using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.ValueParsers
{
    internal class IntAndStringParser : IValueParser
    {
        public bool Parse(List<Syntax.Token> code, int index, int length, Expressions.ExpressionParser ep, out AST.IExpression val)
        {
            if (length == 1)
            {
                switch (code[index].Command)
                {
                    case "Int":
                        val = new AST.Expressions.ValueExpression(new AST.Values.VInteger(Int32.Parse(code[index].Argument)),null);
                        return true;
                    case "String":
                        val = new AST.Expressions.ValueExpression(new AST.Values.VString(code[index].Argument),null);
                        return true;
                }
            }
            val = null;
            return false;
        }
    }
}
