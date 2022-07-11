using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class GetValueExpression:IExpression,IUnaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public GetValueExpression(ExpressionContainer addr, ExpressionContainer parent)
        {
            this.AExp = addr;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            IValue vA = AExp.Expression.Eval(c);
            IExpression exp;
            LinkedListNode<ICommand> node;
            if(vA.GetNode(out node))
            {
                if(node.Value is Commands.StoreCommand)
                {
                    return (node.Value as Commands.StoreCommand).Data;
                }
                return new Values.VString(node.Value.Stringify());
            }
            if(vA.GetExpression(out exp))
            {
                return exp.Eval(c);
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Value({AExp.Stringify()})";
        }
    }
}
