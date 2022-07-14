﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Math
{
    internal class SumExpression : IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public SumExpression(ExpressionContainer a, ExpressionContainer b,ExpressionContainer parent)
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
            AExp.TryOptimise();
            AExp.TryOptimise();
            if(IsConstant())
            {
                return new ValueExpression(Eval(null), this.Parent);
            }
            else if (AExp.Expression is SumExpression && (BExp.Expression is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant())
            {
                SumExpression ASum = AExp.Expression as SumExpression;
                if (ASum.BExp.Expression is IOptimizableExpression && (ASum.BExp.Expression as IOptimizableExpression).IsConstant())
                {
                    ASum.BExp.Expression = new SumExpression(ASum.BExp, this.BExp, ASum.BExp).Optimise();
                    ASum.Parent = this.Parent;
                    return ASum;
                }
            }
            else if(AExp.Expression is DiffExpression && (BExp.Expression is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant())
            {
                DiffExpression ADiff = AExp.Expression as DiffExpression;
                if (ADiff.BExp.Expression is IOptimizableExpression && (ADiff.BExp.Expression as IOptimizableExpression).IsConstant())
                {
                    ADiff.BExp.Expression = new DiffExpression(ADiff.BExp, this.BExp, ADiff.BExp).Optimise();
                    ADiff.Parent = this.Parent;
                    return ADiff;
                }
            }
            return this;
        }
    }
}
