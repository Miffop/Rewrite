using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class InputCommand:ICommand
    {
        IExpression Address;
        public InputCommand(IExpression address,int ln)
        {
            this.Address = address;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> addr;
            if (Address.Eval(c).GetNode(out addr))
            {
                addr.Value = new StoreCommand(new Values.VString(Console.ReadLine()), addr.Value.Line);
            }
            else
            {
                throw new Exception("Address expected");
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"Input({Address.Stringify()})";
        }
    }
}
