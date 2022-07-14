using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Math
{
    internal class MulExpression:IOptimizableExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public MulExpression(ExpressionContainer a, ExpressionContainer b,ExpressionContainer parent)
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
            else if(AExp.Expression is MulExpression && (BExp.Expression is IOptimizableExpression) && (BExp.Expression as IOptimizableExpression).IsConstant())
            {
                MulExpression AMul = (AExp.Expression as MulExpression);
                if((AMul.BExp.Expression is IOptimizableExpression) && (AMul.BExp.Expression as IOptimizableExpression).IsConstant())
                {
                    AMul.BExp.Expression = new MulExpression(AMul.BExp, this.BExp,AMul.BExp).Optimise();
                    AMul.Parent = this.Parent;
                    return AMul;
                }
            }
            else if (BExp.Expression is MulExpression && (AExp.Expression is IOptimizableExpression) && (AExp.Expression as IOptimizableExpression).IsConstant())
            {
                MulExpression BMul = (BExp.Expression as MulExpression);
                if ((BMul.AExp.Expression is IOptimizableExpression) && (BMul.AExp.Expression as IOptimizableExpression).IsConstant())
                {
                    BMul.AExp.Expression = new MulExpression(BMul.AExp, this.AExp,BMul.AExp).Optimise();
                    BMul.Parent = this.Parent;
                    return BMul;
                }
            }
            return this;
        }
    }
}
