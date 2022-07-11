using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Comparison
{
    internal class LessExpression:IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public LessExpression(ExpressionContainer a, ExpressionContainer b,ExpressionContainer parent)
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
            LinkedListNode<ICommand> nA, nB;
            if(vA.GetInteger(out iA) && vB.GetInteger(out iB))
            {
                return new Values.VInteger(iA < iB ? 1 : 0);
            }
            if(vA.GetString(out sA) && vB.GetString(out sB))
            {
                return new Values.VInteger(String.Compare(sA, sB) < 0 ? 1 : 0);
            }
            if(vA.GetNode(out nA) && vB.GetNode(out nB))
            {
                return new Values.VInteger(nA.Value.Line < nB.Value.Line ? 1 : 0);
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} < {BExp.Stringify()})";
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
