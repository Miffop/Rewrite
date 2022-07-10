using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Comparison
{
    internal class EqualExpression:IOptimizableExpression,IBinaryExpression
    {
        public IExpression AExp { get; set; }
        public IExpression BExp { get; set; }
        public EqualExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue vA = AExp.Eval(c);
            IValue vB = BExp.Eval(c);
            int iA, iB;
            string sA, sB;
            LinkedListNode<ICommand> nA, nB;
            if(vA.GetInteger(out iA) && vB.GetInteger(out iB))
            {
                return new Values.VInteger(iA == iB ? 1 : 0);
            }
            if(vA.GetString(out sA) && vB.GetString(out sB))
            {
                return new Values.VInteger(sA == sB ? 1 : 0);
            }
            if(vA.GetNode(out nA) && vB.GetNode(out nB))
            {
                return new Values.VInteger(nA == nB ? 1 : 0);
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} == {BExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return
                (AExp is IOptimizableExpression) && (AExp as IOptimizableExpression).IsConstant() &&
                (BExp is IOptimizableExpression) && (BExp as IOptimizableExpression).IsConstant();

        }
        public IExpression Optimise()
        {
            if (AExp is IOptimizableExpression)
            {
                AExp = (AExp as IOptimizableExpression).Optimise();
            }
            if (BExp is IOptimizableExpression)
            {
                BExp = (BExp as IOptimizableExpression).Optimise();
            }
            if (IsConstant())
            {
                return new ValueExpression(this.Eval(null));
            }
            return this;
        }
    }
}
