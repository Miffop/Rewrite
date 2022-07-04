using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions
{
    internal class ValueExpression:IOptimizableExpression
    {
        IValue val;
        public ValueExpression(IValue v)
        {
            this.val = v;
        }
        public IValue Eval(Context c)
        {
            return val;
        }
        public string Stringify()
        {
            return $"{val.Stringify()}";
        }

        public bool IsConstant()
        {
            return true;
        }
        public IExpression Optimise()
        {
            return this;
        }
    }
}
