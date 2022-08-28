using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Conversion
{
    internal class ToCharExpression:IExpression,IUnaryOperation,IOptimizableExpression
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ToCharExpression(ExpressionContainer exp,ExpressionContainer parent)
        {
            this.Parent = parent;
            this.AExp = exp;
        }
        public IValue Eval(Context c)
        {
            IValue vA = this.AExp.Expression.Eval(c);
            if(vA.GetInteger(out int iA))
            {
                return new Values.VString(((char)iA).ToString());
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Char({this.AExp.Expression.Stringify()})";
        }
        public bool IsConstant()
        {
            return (this.AExp.Expression is IOptimizableExpression) && (this.AExp.Expression as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            if (this.IsConstant())
            {
                return new ValueExpression(this.Eval(null),this.Parent);
            }
            return this;
        }
    }
}
