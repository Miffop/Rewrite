using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Reflection
{
    internal class LineExpression:IExpression,IUnaryOperation
    {
        public ExpressionContainer Parent { get; set; }
        public ExpressionContainer AExp { get; set; }
        public LineExpression(ExpressionContainer node,ExpressionContainer parent)
        {
            this.AExp = node;
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            LinkedListNode<ICommand> n;
            if(AExp.Expression.Eval(c).GetNode(out n))
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
