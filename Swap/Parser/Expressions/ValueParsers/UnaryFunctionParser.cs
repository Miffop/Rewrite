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
                    code[index].Argument == "Int" ||
                    code[index].Argument == "String" ||
                    code[index].Argument == "Length" ||
                    code[index].Argument == "Char"
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
                        case "String":
                            exp = new AST.Expressions.Conversion.ToStringExpression(Base, null);
                            break;
                        case "Length":
                            exp = new AST.Expressions.Strings.LengthExp(Base, null);
                            break;
                        case "Char":
                            exp = new AST.Expressions.Conversion.ToCharExpression(Base, null);
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
