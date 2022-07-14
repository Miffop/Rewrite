using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST
{
    internal class Context
    {
        public ProgramList Root;
        public ProgramList Current;
        public int ExecutingLine;
        
        public Context(ProgramList Root,ProgramList Current)
        {
            this.Root = Root;
            this.Current = Current;
            this.ExecutingLine = -1;
        }

    }
}
