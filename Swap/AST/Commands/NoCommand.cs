using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands
{
    internal class NoCommand:ICommand
    {
        public NoCommand(int ln)
        {
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            return Parent.Next;
        }
        public override string Stringify()
        {
            return "Nope();";
        }
    }
}
