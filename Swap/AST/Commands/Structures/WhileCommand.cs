using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands.Structures
{
    internal class WhileCommand:ProgramList,IUnaryOperation
    {
        public ExpressionContainer AExp { get; set; }
        public WhileCommand(ExpressionContainer A,int ln) : base(ln)
        {
            this.AExp = A;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            while (AExp.Expression.Eval(c).GetInteger(out int cond) && cond!=0)
            {
                base.Exec(c);
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"while({AExp.Stringify()})\n{base.Stringify()}";
        }
    }
}
