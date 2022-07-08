using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class GetValueExpression:IExpression
    {
        IExpression Address;
        public GetValueExpression(IExpression addr)
        {
            this.Address = addr;
        }
        public IValue Eval(Context c)
        {
            LinkedListNode<ICommand> node;
            if(Address.Eval(c).GetNode(out node))
            {
                if(node.Value is Commands.StoreCommand)
                {
                    return (node.Value as Commands.StoreCommand).Data;
                }
                return new Values.VString(node.Value.Stringify());
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Value({Address.Stringify()})";
        }
    }
}
