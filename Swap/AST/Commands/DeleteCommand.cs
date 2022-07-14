using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands
{
    internal class DeleteCommand:ICommand,IUnaryOperation
    {
        public ExpressionContainer AExp { get; set; }//Address
        public DeleteCommand(ExpressionContainer addr,int ln)
        {
            this.AExp = addr;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> com;
            if(AExp.Expression.Eval(c).GetNode(out com))
            {
                /*
                com.Value = new NoCommand(com.Value.Line);
                com.Value.Parent = com;
                */
                com.List.Remove(com);
                return Parent.Next;
            }
            throw new Exception($"Address expected {this.Stringify()}");
        }
        public override string Stringify()
        {
            return $"Delete({AExp.Stringify()});";
        }
    }

}
