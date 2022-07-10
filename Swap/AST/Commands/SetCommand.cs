using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class SetCommand:ICommand,IBinaryOperation
    {
        public IExpression AExp { get; set; }//Address
        public IExpression BExp { get; set; }//Value
        public SetCommand(IExpression addr,IExpression val,int ln)
        {
            this.AExp = addr;
            this.BExp = val;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue vA = AExp.Eval(c);
            IExpression exp;
            LinkedListNode<ICommand> node;
            if(vA.GetNode(out node))
            {
                if(node.Value is StoreCommand)
                {
                    (node.Value as StoreCommand).Data = BExp.Eval(c);
                }
                else
                {
                    node.Value = new StoreCommand(BExp.Eval(c), node.Value.Line);
                    node.Value.Parent = node;
                }
                return Parent.Next;
            }
            if(vA.GetExpression(out exp))
            {
                if(exp is Expressions.ValueExpression)
                {
                    (exp as Expressions.ValueExpression).Value=BExp.Eval(c);
                }
                else
                {
                    throw new NotImplementedException();
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
