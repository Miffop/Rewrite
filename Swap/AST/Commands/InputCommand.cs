using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class InputCommand:ICommand,IUnaryOperation
    {
        public IExpression AExp { get; set; }//Address
        public InputCommand(IExpression address,int ln)
        {
            this.AExp = address;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> addr;
            if (AExp.Eval(c).GetNode(out addr))
            {
                addr.Value = new StoreCommand(new Values.VString(Console.ReadLine()), addr.Value.Line);
                addr.Value.Parent = addr;
            }
            else
            {
                throw new Exception("Address expected");
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"Input({AExp.Stringify()})";
        }
    }
}
