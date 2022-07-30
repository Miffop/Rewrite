using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands.Structures
{
    internal class IFCommand:ProgramList,IUnaryOperation
    {
        public ExpressionContainer AExp { get; set; }
        public IFCommand(ExpressionContainer cond,int ln):base(ln)
        {
            this.AExp = cond;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            if(AExp.Expression.Eval(c).GetInteger(out int cond) && cond != 0)
            {
                base.Exec(c);
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"If({AExp.Stringify()})\n{base.Stringify()}";
        }
    }
}
