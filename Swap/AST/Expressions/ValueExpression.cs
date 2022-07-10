using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions
{
    internal class ValueExpression:IOptimizableExpression
    {
        public IValue Value { get; set; }
        public ValueExpression(IValue v)
        {
            this.Value = v;
        }
        public IValue Eval(Context c)
        {
            return Value;
        }
        public string Stringify()
        {
            return $"{Value.Stringify()}";
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
