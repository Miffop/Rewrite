using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.OperationParsers
{
    internal class ReflectionOperationParser:IOperationParser
    {
        public bool GetPriority(string operation,out int i)
        {
            switch (operation)
            {
                case ":":
                case "::":
                    i = 5;
                    i = 5;
                    return true;
            }
            i = -1;
            return false;
        }
        public AST.IExpression Parse(string operation,AST.ExpressionContainer A,AST.ExpressionContainer B)
        {
            switch (operation)
            {
                case ":":
                    return new AST.Expressions.Reflection.SublineExpression(A, B, null);
                case "::":
                    return new AST.Expressions.Reflection.SubExpExpression(A, B, null);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
