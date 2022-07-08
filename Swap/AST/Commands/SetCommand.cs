using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class SetCommand:ICommand
    {
        IExpression Address;
        IExpression Value;
        public SetCommand(IExpression addr,IExpression val,int ln)
        {
            this.Address = addr;
            this.Value = val;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> node;
            if(Address.Eval(c).GetNode(out node))
            {
                if(node.Value is StoreCommand)
                {
                    (node.Value as StoreCommand).Data = Value.Eval(c);
                }
                else
                {
                    node.Value = new StoreCommand(Value.Eval(c), node.Value.Line);
                    node.Value.Parent = node;
                }
                return Parent.Next;
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public override string Stringify()
        {
            return $"Set({Address.Stringify()},{Value.Stringify()});";
        }
    }
}
