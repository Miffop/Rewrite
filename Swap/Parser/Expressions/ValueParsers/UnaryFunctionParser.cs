using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Expressions.ValueParsers
{
    internal class UnaryFunctionParser : IValueParser
    {
        public bool Parse(List<Syntax.Token> code, int index, int length, Expressions.ExpressionParser ep, out AST.IExpression exp)
        {
            if (code[index].Command == "Word")
            {
                if (
                    code[index].Argument == "Line" ||
                    code[index].Argument == "Value" ||
                    code[index].Argument == "Int"
                    )
                {
                    if (code[index + 1].Command != "BraceOpen")
                    {
                        throw new Exception("'(' expected");
                    }
                    if (code[index + length - 1].Command != "BraceClose")
                    {
                        throw new Exception("')' expected");
                    }
                    AST.ExpressionContainer Base = ep.Parse(code, index + 2, length - 3);
                    switch (code[index].Argument)
                    {
                        case "Line":
                            exp = new AST.Expressions.Reflection.LineExpression(Base, null);
                            break;
                        case "Value":
                            exp = new AST.Expressions.Reflection.GetValueExpression(Base, null);
                            break;
                        case "Int":
                            exp = new AST.Expressions.Conversion.ToIntExpression(Base, null);
                            break;
                        default:
                            throw new Exception("This must be a bug");
                    }
                    return true;
                }
            }
            exp = null;
            return false;
        }
    }
}
