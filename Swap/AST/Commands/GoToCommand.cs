using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class GoToCommand:ICommand
    {
        IExpression ToExpression;
        public GoToCommand(IExpression to,int ln)
        {
            this.ToExpression = to;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> To;
            if (ToExpression.Eval(c).GetNode(out To))
            {
                return To;
            }
            else
            {
                throw new Exception("Address Expected");
            }
        }
        public override string Stringify()
        {
            return $"GoTo({ToExpression.Stringify()});";
        }
    }
}
