using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class SublineExpression:IExpression,IBinaryOperation
    {
        public IExpression AExp { get; set; }
        public IExpression BExp { get; set; }
        public SublineExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }

        public IValue Eval(Context c)
        {
            IValue vA = AExp.Eval(c);
            IValue vB = BExp.Eval(c);
            LinkedListNode<ICommand> nA;
            IExpression eA;
            int iB;
            if(vA.GetNode(out nA) && vB.GetInteger(out iB))
            {
                if(nA.Value is ProgramList)
                {
                    return new Values.VNode((nA.Value as ProgramList).Find(iB));
                }
                else if(nA.Value is IUnaryOperation && iB==0)
                {
                    return new Values.VExpression((nA.Value as IUnaryOperation).AExp);
                }
                else if(nA.Value is IBinaryOperation && iB == 1)
                {
                    return new Values.VExpression((nA.Value as IBinaryOperation).BExp);
                }
            }
            if(vA.GetExpression(out eA) && vB.GetInteger(out iB))
            {
                if(eA is IUnaryOperation && iB == 0)
                {
                    return new Values.VExpression((eA as IUnaryOperation).AExp);
                }
                else if(eA is IBinaryOperation && iB == 1)
                {
                    return new Values.VExpression((eA as IBinaryOperation).BExp);
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
