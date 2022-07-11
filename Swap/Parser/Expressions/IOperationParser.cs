using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions
{
    internal interface IOperationParser
    {

        bool GetPriority(string operation,out int priority);
        AST.IExpression Parse(string operation, AST.ExpressionContainer left, AST.ExpressionContainer right);
    }
}
