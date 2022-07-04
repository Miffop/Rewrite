using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class DivExpression:IExpression
    {
        IExpression AExp, BExp;
        public DivExpression(IExpression a,IExpression b)
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
                return new Values.VInteger(iA / iB);
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} / {BExp.Stringify()})";
        }
    }
}
