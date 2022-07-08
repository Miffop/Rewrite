using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class LineExpression:IExpression
    {
        IExpression Node;
        public LineExpression(IExpression node)
        {
            this.Node = node;
        }
        public IValue Eval(Context c)
        {
            LinkedListNode<ICommand> n;
            if(Node.Eval(c).GetNode(out n))
            {
                return new Values.VInteger(n.Value.Line);
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Line({Node.Stringify()})";
        }
    }
}
