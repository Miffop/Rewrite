using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions
{
    internal interface IBinaryExpression:IExpression
    {
        IExpression AExp { get; set; }
        IExpression BExp { get; set; }
    }
}
