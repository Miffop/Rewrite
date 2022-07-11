using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST
{
    internal class ExpressionContainer
    {
        public IExpression Expression;

        public ExpressionContainer(IExpression exp)
        {
            this.Expression = exp;
            this.Expression.Parent = this;
        }
        public string Stringify()
        {
            return Expression.Stringify(); 
        }
        public override string ToString()
        {
            return Stringify();
        }

        public void TryOptimise()
        {
            if(Expression is IOptimizableExpression)
            {
                Expression = (Expression as IOptimizableExpression).Optimise();
            }
        }
    }
}
