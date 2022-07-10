using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions
{
    internal interface IUnaryExpression:IExpression
    {
        IExpression AExp { get; set; }
    }
}
