using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class GetValueExpression:IExpression,IUnaryExpression
    {
        public IExpression AExp { get; set; }
        public GetValueExpression(IExpression addr)
        {
            this.AExp = addr;
        }
        public IValue Eval(Context c)
        {
            LinkedListNode<ICommand> node;
            if(AExp.Eval(c).GetNode(out node))
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
            return $"Value({AExp.Stringify()})";
        }
    }
}
