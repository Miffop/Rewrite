using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class SublineExpression:IExpression,IBinaryExpression
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
            LinkedListNode<ICommand> nA;
            int iB;
            if(AExp.Eval(c).GetNode(out nA) && BExp.Eval(c).GetInteger(out iB))
            {
                return new Values.VNode((nA.Value as ProgramList).Find(iB));
            }
            throw new Exception($"Address and Integer expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} : {BExp.Stringify()})";
        }
    }
}
