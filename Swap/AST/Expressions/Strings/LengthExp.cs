using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Strings
{
    internal class LengthExp:IExpression,IUnaryOperation,IOptimizableExpression
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public LengthExp(ExpressionContainer exp,ExpressionContainer parent)
        {
            this.Parent = parent;
            this.AExp = exp;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Expression.Eval(c);
            if (A.GetString(out string sA))
            {
                return new Values.VInteger(sA.Length);
            }
            throw new Exception($"Cannot perform {this.Stringify()}");
        }
        public bool IsConstant()
        {
            return (AExp.Expression is IOptimizableExpression) && (AExp.Expression as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            if (this.IsConstant())
            {
                return new ValueExpression(this.Eval(null),this.Parent);
            }
            return this;
        }
        public string Stringify()
        {
            return $"Length({AExp.Expression.Stringify()})";
        }
    }
}
