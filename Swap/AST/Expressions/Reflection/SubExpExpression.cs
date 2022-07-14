using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Reflection
{
    internal class SubExpExpression:IExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public SubExpExpression(ExpressionContainer a,ExpressionContainer b,ExpressionContainer parent) 
        {
            this.AExp = a;
            this.BExp = b;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            IValue vA = AExp.Expression.Eval(c);
            IValue vB = BExp.Expression.Eval(c);

            if (vA.GetNode(out LinkedListNode<ICommand> nA) && vB.GetInteger(out int iB))
            {
                if (nA.Value is IUnaryOperation && iB == 0)
                {
                    return new Values.VExpression((nA.Value as IUnaryOperation).AExp.Expression);
                }
                else if (nA.Value is IBinaryOperation && iB == 1)
                {
                    return new Values.VExpression((nA.Value as IBinaryOperation).BExp.Expression);
                }
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()}::{BExp.Stringify()})";
        }

    }
}
