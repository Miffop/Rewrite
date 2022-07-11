using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class SwapCommand:ICommand,IBinaryOperation
    {
        public ExpressionContainer AExp { get; set; }
        public ExpressionContainer BExp { get; set; }

        public SwapCommand(ExpressionContainer A, ExpressionContainer B,int ln)
        {
            this.AExp = A;
            this.BExp = B;
            this.Line = ln;
        }

        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue lineA = AExp.Expression.Eval(c);
            IValue lineB = BExp.Expression.Eval(c);
            
            LinkedListNode<ICommand> nA, nB;
            IExpression eA, eB;
            if(lineA.GetNode(out nA) && lineB.GetNode(out nB))
            {
                {
                    ICommand I = nA.Value;
                    nA.Value = nB.Value;
                    nB.Value = I;
                }
                {
                    int i = nA.Value.Line;
                    nA.Value.Line = nB.Value.Line;
                    nB.Value.Line = i;
                }
                {
                    nA.Value.Parent = nB;
                    nB.Value.Parent = nA;
                }
                return Parent.Next;
            }
            else if(lineA.GetExpression(out eA) && lineB.GetExpression(out eB))
            {
                ExpressionContainer ecA = eA.Parent;
                ExpressionContainer ecB = eB.Parent;
                {
                    IExpression I = ecA.Expression;
                    ecA.Expression = ecB.Expression;
                    ecB.Expression = I;
                }
                {
                    eA.Parent = ecB;
                    eB.Parent = ecA;
                }
                return Parent.Next;
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public override string Stringify()
        {
            return $"Swap({this.AExp.Stringify()},{this.BExp.Stringify()});";
        }
    }

}
