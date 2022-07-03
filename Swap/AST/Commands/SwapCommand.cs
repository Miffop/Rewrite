using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class SwapCommand:ICommand
    {
        IExpression LineAExpression;
        IExpression LineBExpression;

        public SwapCommand(IExpression A,IExpression B,int ln)
        {
            this.LineAExpression = A;
            this.LineBExpression = B;
            this.Line = ln;
        }

        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue lineA = LineAExpression.Eval(c);
            IValue lineB = LineBExpression.Eval(c);
            
            LinkedListNode<ICommand> A, B;
            
            if(lineA.GetNode(out A) && lineB.GetNode(out B))
            {
                {
                    ICommand I = A.Value;
                    A.Value = B.Value;
                    B.Value = I;
                }
                {
                    int i = A.Value.Line;
                    A.Value.Line = B.Value.Line;
                    B.Value.Line = i;
                }
                {
                    A.Value.Parent = B;
                    B.Value.Parent = A;
                }
                return Parent.Next;
            }
            else
            {
                throw new Exception("Address Expected");
            }
        }
        public override string Stringify()
        {
            return $"Swap({this.LineAExpression.Stringify()},{this.LineBExpression.Stringify()});";
        }
    }

}
