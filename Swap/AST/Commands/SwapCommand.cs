using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class SwapCommand:ICommand,IBinaryOperation
    {
        public IExpression AExp { get; set; }
        public IExpression BExp { get; set; }

        public SwapCommand(IExpression A,IExpression B,int ln)
        {
            this.AExp = A;
            this.AExp = B;
            this.Line = ln;
        }

        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue lineA = AExp.Eval(c);
            IValue lineB = AExp.Eval(c);
            
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
            return $"Swap({this.AExp.Stringify()},{this.AExp.Stringify()});";
        }
    }

}
