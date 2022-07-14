using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions
{
    internal class ValueExpression:IOptimizableExpression
    {
        public ExpressionContainer Parent { get; set; }
        public IValue Value { get; set; }
        public ValueExpression(IValue v,ExpressionContainer parent)
        {
            this.Value = v;
            this.Parent = parent;
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
