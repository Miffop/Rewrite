using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Math
{
    internal class DiffExpression: IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public DiffExpression(ExpressionContainer a,ExpressionContainer b,ExpressionContainer parent)
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
            LinkedListNode<ICommand> nA;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA - iB);
            }
            else if(A.GetNode(out nA) && B.GetInteger(out iB))
            {
                return new Values.VNode(nA.Move(-iB));
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} - {BExp.Stringify()})";
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
                return new ValueExpression(Eval(null),this.Parent);
            }
            else if (AExp.Expression is SumExpression && (BExp is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant())
            {
                SumExpression ASum = AExp.Expression as SumExpression;
                if (ASum.BExp.Expression is IOptimizableExpression && (ASum.BExp.Expression as IOptimizableExpression).IsConstant())
                {
                    ASum.BExp.Expression = new DiffExpression(ASum.BExp, this.BExp, ASum.BExp).Optimise();
                    ASum.Parent = this.Parent;
                    return ASum;
                }
            }
            else if (AExp.Expression is DiffExpression && (BExp.Expression is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant())
            {
                DiffExpression ADiff = AExp.Expression as DiffExpression;
                if (ADiff.BExp is IOptimizableExpression && (ADiff.BExp as IOptimizableExpression).IsConstant())
                {
                    ADiff.BExp.Expression = new SumExpression(ADiff.BExp, this.BExp, ADiff.BExp).Optimise();
                    ADiff.Parent = this.Parent;
                    return ADiff;
                }
            }
            return this;
        }
    }
}
