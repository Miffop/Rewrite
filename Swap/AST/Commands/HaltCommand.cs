using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Commands
{
    internal class HaltCommand:ICommand
    {
        public HaltCommand(int ln)
        {
            this.Line = ln;
        }
        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            return null;
        }
        public override string Stringify()
        {
            return "Halt();";
        }
    }
}
