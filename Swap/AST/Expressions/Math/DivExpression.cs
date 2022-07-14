using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Math
{
    internal class DivExpression:IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public DivExpression(ExpressionContainer a, ExpressionContainer b, ExpressionContainer parent)
        {
            this.AExp = a;
            this.BExp = b;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Expression.Eval(c);
            IValue B = BExp.Expression.Eval(c);

            int iA, iB;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA / iB);
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} / {BExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return
                AExp.Expression is IOptimizableExpression &&
                BExp.Expression is IOptimizableExpression &&
                (AExp.Expression as IOptimizableExpression).IsConstant() &&
                (BExp.Expression as IOptimizableExpression).IsConstant();

        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            BExp.TryOptimise();
            if (IsConstant())
            {
                return new ValueExpression(Eval(null), this.Parent);
            }
            else
            {
                return this;
            }
        }
    }
}
