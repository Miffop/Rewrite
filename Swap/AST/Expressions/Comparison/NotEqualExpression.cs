using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Comparison
{
    internal class NotEqualExpression:IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public NotEqualExpression(ExpressionContainer a, ExpressionContainer b,ExpressionContainer parent)
        {
            this.AExp = a;
            this.BExp = b;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            IValue vA = AExp.Expression.Eval(c);
            IValue vB = BExp.Expression.Eval(c);
            int iA, iB;
            string sA, sB;
            IExpression eA, eB;
            LinkedListNode<ICommand> nA, nB;
            if(vA.GetInteger(out iA) && vB.GetInteger(out iB))
            {
                return new Values.VInteger(iA != iB ? 1 : 0);
            }
            if(vA.GetString(out sA) && vB.GetString(out sB))
            {
                return new Values.VInteger(sA != sB ? 1 : 0);
            }
            if(vA.GetNode(out nA) && vB.GetNode(out nB))
            {
                return new Values.VInteger(nA != nB ? 1 : 0);
            }
            if (vA.GetExpression(out eA) && vB.GetExpression(out eB))
            {
                return new Values.VInteger(eA == eB ? 1 : 0);
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} != {BExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return
                (AExp.Expression is IOptimizableExpression) && (AExp.Expression as IOptimizableExpression).IsConstant() &&
                (BExp.Expression is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant();

        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            BExp.TryOptimise();
            if (IsConstant())
            {
                return new ValueExpression(this.Eval(null), this.Parent);
            }
            return this;
        }
    }
}
