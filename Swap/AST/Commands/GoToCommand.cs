using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class GoToCommand:ICommand,IUnaryOperation
    {
        public ExpressionContainer AExp { get; set; }//Address
        public GoToCommand(ExpressionContainer to,int ln)
        {
            this.AExp = to;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            throw new Exception("GoTos are hard to read\nbranching and loops are better\nSo, let's ban GoTos!!!\n");
            /*LinkedListNode<ICommand> To;
            if (AExp.Expression.Eval(c).GetNode(out To))
            {
                return To;
            }
            else
            {
                throw new Exception("Address Expected");
            }*/
        }
        public override string Stringify()
        {
            return $"GoTo({AExp.Stringify()});";
        }
    }
}
