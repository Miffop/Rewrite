using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Commands
{
    internal class PrintCommand:ICommand
    {
        IExpression Address;
        public PrintCommand(IExpression address,int ln)
        {
            this.Address = address;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            LinkedListNode<ICommand> addr;
            if (Address.Eval(c).GetNode(out addr))
            {
                if(addr.Value is StoreCommand)
                {
                    StoreCommand store=addr.Value as StoreCommand;
                    string s;
                    if(!store.Data.GetString(out s))
                    {
                        s = store.Stringify();
                    }
                    Console.Write(s);
                }
                else
                {
                    Console.Write(addr.Value.Stringify());
                }
            }
            else
            {
                throw new Exception("Address expected");
            }
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"Print({Address.Stringify()});";
        }
    }
}
