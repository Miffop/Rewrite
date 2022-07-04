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
                    priority = 1;
                    return true;
            }
            priority = -1;
            return false;
        }
        public AST.IExpression Parse(string operation,AST.IExpression left,AST.IExpression right)
        {
            switch (operation)
            {
                case "+":
                    return new AST.Expressions.Math.SumExpression(left, right);
                case "-":
                    return new AST.Expressions.Math.DiffExpression(left, right);
                case "*":
                    return new AST.Expressions.Math.MulExpression(left, right);
                case "/":
                    return new AST.Expressions.Math.DivExpression(left, right);
                default:
                    throw new Exception($"Cannot parse operation: '{operation}'");
            }
        }
    }
}
