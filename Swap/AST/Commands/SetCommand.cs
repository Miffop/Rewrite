using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands
{
    internal class SetCommand:ICommand,IBinaryOperation
    {
        public ExpressionContainer AExp { get; set; }//Address
        public ExpressionContainer BExp { get; set; }//Value
        public SetCommand(ExpressionContainer addr, ExpressionContainer val,int ln)
        {
            this.AExp = addr;
            this.BExp = val;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue vA = AExp.Expression.Eval(c);
            IExpression exp;
            LinkedListNode<ICommand> node;
            if(vA.GetNode(out node))
            {
                if(node.Value is StoreCommand)
                {
                    (node.Value as StoreCommand).Data = BExp.Expression.Eval(c);
                }
                else
                {
                    node.Value = new StoreCommand(BExp.Expression.Eval(c), node.Value.Line);
                    node.Value.Parent = node;
                }
                return Parent.Next;
            }
            if(vA.GetExpression(out exp))
            {
                if(exp is Expressions.ValueExpression)
                {
                    (exp as Expressions.ValueExpression).Value=BExp.Expression.Eval(c);
                }
                else
                {
                    IValue vB = BExp.Expression.Eval(c);
                    IExpression eB;
                    if(!vB.GetExpression(out eB))
                    {
                        eB = new Expressions.ValueExpression(vB, exp.Parent);
                    }
                    else
                    {
                        eB.Parent = exp.Parent;
                    }
                    exp.Parent.Expression = eB;
                    
                }
                return Parent.Next;
            }
            throw new Exception($"Address expected: {this.Stringify()}");
        }
        public override string Stringify()
        {
            return $"Set({AExp.Stringify()},{BExp.Stringify()});";
        }
    }
}
