using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Reflection
{
    internal class GetValueExpression:IExpression,IUnaryOperation,IOptimizableExpression
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
            
            if(vA.GetNode(out LinkedListNode<ICommand> node))
            {
                if(node.Value is Commands.StoreCommand)
                {
                    return (node.Value as Commands.StoreCommand).Data;
                }
                return new Values.VString(node.Value.Stringify());
            }
            if(vA.GetExpression(out IExpression exp))
            {
                return exp.Eval(c);
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public string Stringify()
        {
            return $"Value({AExp.Stringify()})";
        }
        public bool IsConstant()
        {
            return (AExp.Expression is ValueExpression) && (AExp.Expression as ValueExpression).Value is Values.VExpression;
        }
        public IExpression Optimise()
        {
            if (IsConstant())
            {
                (AExp.Expression as ValueExpression).Value.GetExpression(out IExpression exp);
                return exp;
            }
            return this;
        }
    }
}
