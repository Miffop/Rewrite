using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions
{
    internal class InputExpression:IExpression
    {
        public ExpressionContainer Parent { get; set; }
        public InputExpression(ExpressionContainer parent)
        {
            this.Parent = parent;
        }
        public IValue Eval(Context c)
        {
            return new Values.VString(Console.ReadLine());
        }
        public string Stringify()
        {
            return $"Input()";
        }
    }
}
