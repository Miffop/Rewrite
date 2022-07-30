using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Conversion
{
    internal class ToStringExpression:IExpression,IUnaryOperation,IOptimizableExpression
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ToStringExpression(ExpressionContainer exp, ExpressionContainer parent)
        {
            this.AExp = exp;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            IValue val = AExp.Expression.Eval(c);
            if (val.GetInteger(out int iA))
            {
                return new Values.VString(iA.ToString());
            }
            if (val.GetString(out string sA))
            {
                return new Values.VString(sA);
            }
            if(val.GetNode(out LinkedListNode<ICommand> nA))
            {
                return new Values.VString(nA.Value.Stringify());
            }
            if(val.GetExpression(out IExpression eA))
            {
                return new Values.VString(eA.Stringify());
            }
            throw new Exception($"Cannot convert to string: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"String({AExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return (AExp.Expression is IOptimizableExpression) && (AExp.Expression as IOptimizableExpression).IsConstant();
        }
        public IExpression Optimise()
        {
            AExp.TryOptimise();
            if (IsConstant())
            {
                return new ValueExpression(this.Eval(null), this.Parent);
            }
            return this;
        }
    }
}
