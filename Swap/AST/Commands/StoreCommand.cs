using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands
{
    internal class StoreCommand:ICommand
    {
        public IValue Data { get; set; }
        public StoreCommand(IValue data,int ln)
        {
            this.Data = data;
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            return Parent.Next;
        }
        public override string Stringify()
        {
            return $"{Data.Stringify()};";
        }
    }
}
