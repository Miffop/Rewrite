using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class PrintCommand:ICommand,IUnaryOperation
    {
        public IExpression AExp { get; set; }
        public PrintCommand(IExpression address,int ln)
        {
            this.AExp = address;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            IValue vA = AExp.Eval(c);
            int i;
            string s;
            IExpression e;
            LinkedListNode<ICommand> addr;
            if (vA.GetNode(out addr))
            {
                if(addr.Value is StoreCommand)
                {
                    StoreCommand store=addr.Value as StoreCommand;
                    if(!store.Data.GetString(out s))
                    {
                        s = store.Stringify();
                    }
                    Console.Write(s);
                }
                else
                {
                    Console.WriteLine(addr.Value.Stringify());
                }
            }
            else if(vA.GetExpression(out e))
            {
                Console.WriteLine(e.Stringify());
            }
            else if (vA.GetInteger(out i))
            {
                Console.Write(i);
            }
            else if (vA.GetString(out s))
            {
                Console.Write(s);
            }
            else
            {
                throw new Exception($"Address expected: {this.Stringify()}");
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"Print({AExp.Stringify()});";
        }
    }
}
