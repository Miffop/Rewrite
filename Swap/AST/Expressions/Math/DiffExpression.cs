﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class DiffExpression: IOptimizableExpression
    {
        public IExpression AExp, BExp;
        public DiffExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Eval(c);
            IValue B = BExp.Eval(c);

            int iA, iB;
            LinkedListNode<ICommand> nA;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA - iB);
            }
            else if(A.GetNode(out nA) && B.GetInteger(out iB))
            {
                for(int i = 0; i < iB; i++)
                {
                    nA = nA.Previous;
                }
                return new Values.VNode(nA);
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
                AExp is IOptimizableExpression &&
                BExp is IOptimizableExpression &&
                (AExp as IOptimizableExpression).IsConstant() &&
                (BExp as IOptimizableExpression).IsConstant();

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
                return new ValueExpression(Eval(null));
            }
            else if (AExp is SumExpression)
            {
                SumExpression ASum = AExp as SumExpression;
                if (ASum.BExp is IOptimizableExpression && (ASum.BExp as IOptimizableExpression).IsConstant())
                {
                    ASum.BExp = new DiffExpression(ASum.BExp, this.BExp).Optimise();
                    return ASum;
                }
            }
            else if (AExp is DiffExpression)
            {
                DiffExpression ADiff = AExp as DiffExpression;
                if (ADiff.BExp is IOptimizableExpression && (ADiff.BExp as IOptimizableExpression).IsConstant())
                {
                    ADiff.BExp = new SumExpression(ADiff.BExp, this.BExp).Optimise();
                    return ADiff;
                }
            }
            return this;
        }
    }
}
