using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.OperationParsers
{
    internal class MathOperationParser:IOperationParser
    {
        public bool GetPriority(string operation,out int priority)
        {
            switch (operation)
            {
                case "+":
                case "-":
                    priority = 2;
                    return true;
                case "*":
                case "/":
                case "%":
                    priority = 1;
                    return true;
            }
            priority = -1;
            return false;
        }
        public AST.IExpression Parse(string operation,AST.ExpressionContainer left,AST.ExpressionContainer right)
        {
            switch (operation)
            {
                case "+":
                    return new AST.Expressions.Math.SumExpression(left, right, null);
                case "-":
                    return new AST.Expressions.Math.DiffExpression(left, right, null);
                case "*":
                    return new AST.Expressions.Math.MulExpression(left, right, null);
                case "/":
                    return new AST.Expressions.Math.DivExpression(left, right, null);
                case "%":
                    return new AST.Expressions.Math.ModExpression(left, right, null);
                default:
                    throw new Exception($"Cannot parse operation: '{operation}'");
            }
        }
    }
}
