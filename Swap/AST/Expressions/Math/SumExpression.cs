using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class SumExpression : IOptimizableExpression,IBinaryOperation
    {
        public IExpression AExp { get; set; }
        public IExpression BExp { get; set; }
        public SumExpression(IExpression a, IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Eval(c);
            IValue B = BExp.Eval(c);

            int iA, iB;
            string sA, sB;
            LinkedListNode<ICommand> nA;
            if (A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA + iB);
            }
            else if (A.GetString(out sA) && B.GetString(out sB))
            {
                return new Values.VString(sA + sB);
            }
            else if (A.GetNode(out nA) && B.GetInteger(out iB))
            {
                return new Values.VNode(nA.Move(iB));
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} + {BExp.Stringify()})";
        }

        public bool IsConstant()
        {
            return
                AExp is IOptimizableExpression && 
                BExp is IOptimizableExpression &&
                (AExp as IOptimizableExpression).IsConstant() &&
                (BExp as IOptimizableExpression).IsConstant();

        }
        public IExpression Optimise()
        {
            if(AExp is IOptimizableExpression)
            {
                AExp = (AExp as IOptimizableExpression).Optimise();
            }
            if(BExp is IOptimizableExpression)
            {
                BExp = (BExp as IOptimizableExpression).Optimise();
            }
            if(IsConstant())
            {
                return new ValueExpression(Eval(null));
            }
            else if (AExp is SumExpression && (BExp is IOptimizableExpression) && (BExp as IOptimizableExpression).IsConstant())
            {
                SumExpression ASum = AExp as SumExpression;
                if (ASum.BExp is IOptimizableExpression && (ASum.BExp as IOptimizableExpression).IsConstant())
                {
                    ASum.BExp = new SumExpression(ASum.BExp, this.BExp).Optimise();
                    return ASum;
                }
            }
            else if(AExp is DiffExpression && (BExp is IOptimizableExpression) && (BExp as IOptimizableExpression).IsConstant())
            {
                DiffExpression ADiff = AExp as DiffExpression;
                if (ADiff.BExp is IOptimizableExpression && (ADiff.BExp as IOptimizableExpression).IsConstant())
                {
                    ADiff.BExp = new DiffExpression(ADiff.BExp, this.BExp).Optimise();
                    return ADiff;
                }
            }
            return this;
        }
    }
}
