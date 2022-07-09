﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.OperationParsers
{
    internal class ComparisonOperationParser:IOperationParser
    {
        public bool GetPriority(string operation,out int i)
        {
            switch (operation)
            {
                case "!=":
                case "==":
                    i = 4;
                    return true;
                case ">":
                case "<":
                case ">=":
                case "<=":
                    i = 5;
                    return true;
                default:
                    i = -1;
                    return false;
            }
        }
        public AST.IExpression Parse(string operation,AST.IExpression A, AST.IExpression B)
        {
            switch (operation)
            {
                case "==":
                    return new AST.Expressions.Comparison.EqualExpression(A, B);
                case "!=":
                    return new AST.Expressions.Comparison.NotEqualExpression(A, B);
                case ">":
                    return new AST.Expressions.Comparison.GreaterExpression(A, B);
                case ">=":
                    return new AST.Expressions.Comparison.GreaterEqualExpression(A, B);
                case "<":
                    return new AST.Expressions.Comparison.LessExpression(A, B);
                case "<=":
                    return new AST.Expressions.Comparison.LessEqualExpression(A, B);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
