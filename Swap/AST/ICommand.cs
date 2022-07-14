using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST
{
    internal abstract class ICommand
    {
        public int Line { get; set; }
        public LinkedListNode<ICommand> Parent { get; set; }

        public LinkedListNode<ICommand> Execute(Context c)
        {
            c.ExecutingLine = Line;
            return Exec(c);
        }
        protected abstract LinkedListNode<ICommand> Exec(Context c);
        public abstract string Stringify();
    }
}
