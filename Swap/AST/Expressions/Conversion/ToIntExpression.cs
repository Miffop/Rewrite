using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Conversion
{
    internal class ToIntExpression:IOptimizableExpression
    {
        IExpression Exp;
        public ToIntExpression(IExpression exp)
        {
            this.Exp = exp;
        }
        public IValue Eval(Context c)
        {
            int result;
            IValue val = Exp.Eval(c);
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
            throw new Exception($"Cannot convert to integer: {Exp.Stringify()}");
        }
        public string Stringify()
        {
            return $"Int({Exp.Stringify()})";
        }
        public bool IsConstant()
        {
            return (Exp is IOptimizableExpression) && (Exp as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            if(Exp is IOptimizableExpression)
            {
                Exp = (Exp as IOptimizableExpression).Optimise();
            }
            if(IsConstant())
            {
                return new ValueExpression(this.Eval(null));
            }
            return this;
        }
    }
}
