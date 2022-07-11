using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Conversion
{
    internal class ToIntExpression:IOptimizableExpression,IUnaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ToIntExpression(ExpressionContainer exp,ExpressionContainer parent)
        {
            this.AExp = exp;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            int result;
            IValue val = AExp.Expression.Eval(c);
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
            return (AExp.Expression is IOptimizableExpression) && (AExp.Expression as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            if(IsConstant())
            {
                return new ValueExpression(this.Eval(null), this.Parent);
            }
            return this;
        }
    }
}
