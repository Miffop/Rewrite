using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions
{
    internal class RootNodeExpression:IExpression
    {
        public ExpressionContainer Parent { get; set; }
        public RootNodeExpression(ExpressionContainer parent)
        {
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            return new Values.VNode(c.Root.Parent);
        }
        public string Stringify()
        {
            return "root";
        }
    }
}
