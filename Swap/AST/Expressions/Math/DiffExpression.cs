using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class DiffExpression:IExpression
    {
        IExpression AExp, BExp;
        public DiffExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Eval(c);
            IValue B = BExp.Eval(c);

            int iA, iB;
            LinkedListNode<ICommand> nA;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA - iB);
            }
            else if(A.GetNode(out nA) && B.GetInteger(out iB))
            {
                for(int i = 0; i < iB; i++)
                {
                    nA = nA.Previous;
                }
                return new Values.VNode(nA);
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} - {BExp.Stringify()})";
        }
    }
}
