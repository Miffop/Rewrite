using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST
{
    internal interface IOptimizableExpression:IExpression
    {
        bool IsConstant();
        IExpression Optimise();
    }
}
