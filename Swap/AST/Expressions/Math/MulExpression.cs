using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class MulExpression:IOptimizableExpression,IBinaryOperation
    {
        public IExpression AExp { get; set; }
        public IExpression BExp { get; set; }
        public MulExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Eval(c);
            IValue B = BExp.Eval(c);

            int iA, iB;
            string sA,sB;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA * iB);
            }
            else if (A.GetString(out sA) && B.GetInteger(out iB))
            {
                if (iB < 0)
                {
                    iB = -iB;
                    sA = String.Join("", sA.Split().Reverse());
                }
                string res = "";
                for (int i = 0; i < iB; i++)
                {
                    res += sA;
                }
                return new Values.VString(res);
            }
            else if (A.GetInteger(out iA) && B.GetString(out sB))
            {
                if (iA < 0)
                {
                    iA = -iA;
                    sB = String.Join("", sB.Reverse());
                }
                string res = "";
                for (int i = 0; i < iA; i++)
                {
                    res += sB;
                }
                return new Values.VString(res);
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} * {BExp.Stringify()})";
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
            else if(AExp is MulExpression && (BExp is IOptimizableExpression) && (BExp as IOptimizableExpression).IsConstant())
            {
                MulExpression AMul = (AExp as MulExpression);
                if((AMul.BExp is IOptimizableExpression) && (AMul.BExp as IOptimizableExpression).IsConstant())
                {
                    AMul.BExp = new MulExpression(AMul.BExp, this.BExp).Optimise();
                    return AMul;
                }
            }
            else if (BExp is MulExpression && (AExp is IOptimizableExpression) && (AExp as IOptimizableExpression).IsConstant())
            {
                MulExpression BMul = (BExp as MulExpression);
                if ((BMul.AExp is IOptimizableExpression) && (BMul.AExp as IOptimizableExpression).IsConstant())
                {
                    BMul.AExp = new MulExpression(BMul.AExp, this.AExp).Optimise();
                    return BMul;
                }
            }
            return this;
        }
    }
}
