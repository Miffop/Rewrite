using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Expressions
{
    internal interface IValueParser
    {
        bool Parse(List<Syntax.Token> code, int index, int length, Expressions.ExpressionParser ep, out AST.IExpression val);
    }
}
