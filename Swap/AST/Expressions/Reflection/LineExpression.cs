using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class LineExpression:IExpression,IUnaryExpression
    {
        public IExpression AExp { get; set; }
        public LineExpression(IExpression node)
        {
            this.AExp = node;
        }
        public IValue Eval(Context c)
        {
            LinkedListNode<ICommand> n;
            if(AExp.Eval(c).GetNode(out n))
            {
                return new Values.VInteger(n.Value.Line);
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Line({AExp.Stringify()})";
        }
    }
}
