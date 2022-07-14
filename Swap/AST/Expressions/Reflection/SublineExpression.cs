using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class SublineExpression:IExpression,IBinaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }
        public SublineExpression(ExpressionContainer a, ExpressionContainer b,ExpressionContainer parent)
        {
            this.AExp = a;
            this.BExp = b;
            this.Parent = parent;
        }

        public IValue Eval(Context c)
        {
            IValue vA = AExp.Expression.Eval(c);
            IValue vB = BExp.Expression.Eval(c);
            LinkedListNode<ICommand> nA;
            IExpression eA;
            int iB;
            if(vA.GetNode(out nA) && vB.GetInteger(out iB))
            {
                if(nA.Value is ProgramList)
                {
                    return new Values.VNode((nA.Value as ProgramList).Find(iB));
                }
            }
            if(vA.GetExpression(out eA) && vB.GetInteger(out iB))
            {
                if(eA is IUnaryOperation && iB == 0)
                {
                    return new Values.VExpression((eA as IUnaryOperation).AExp.Expression);
                }
                else if(eA is IBinaryOperation && iB == 1)
                {
                    return new Values.VExpression((eA as IBinaryOperation).BExp.Expression);
                }
            }
            throw new Exception($"Cannot perform: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} : {BExp.Stringify()})";
        }
    }
}
