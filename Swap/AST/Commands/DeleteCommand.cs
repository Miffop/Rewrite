using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class DeleteCommand:ICommand
    {
        IExpression Address;
        public DeleteCommand(IExpression addr,int ln)
        {
            this.Address = addr;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> com;
            if(Address.Eval(c).GetNode(out com))
            {
                com.Value = new NoCommand(com.Value.Line);
                com.Value.Parent = com;
                return Parent.Next;
            }
            throw new Exception($"Address expected {this.Stringify()}");
        }
        public override string Stringify()
        {
            return $"Delete({Address.Stringify()});";
        }
    }

}
