using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Conversion
{
    internal class ToIntExpression:IOptimizableExpression,IUnaryOperation
    {
        public IExpression AExp { get; set; }
        public ToIntExpression(IExpression exp)
        {
            this.AExp = exp;
        }
        public IValue Eval(Context c)
        {
            int result;
            IValue val = AExp.Eval(c);
            if(val.GetInteger(out result))
            {
                return new Values.VInteger(result);
            }
            string str;
            if(val.GetString(out str))
            {
                if(Int32.TryParse(str,out result))
                {
                    return new Values.VInteger(result);
                }
            }
            throw new Exception($"Cannot convert to integer: {AExp.Stringify()}");
        }
        public string Stringify()
        {
            return $"Int({AExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return (AExp is IOptimizableExpression) && (AExp as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            if(AExp is IOptimizableExpression)
            {
                AExp = (AExp as IOptimizableExpression).Optimise();
            }
            if(IsConstant())
            {
                return new ValueExpression(this.Eval(null));
            }
            return this;
        }
    }
}
