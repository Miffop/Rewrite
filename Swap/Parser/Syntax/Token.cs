using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Syntax
{
    internal class Token
    {
        public string Command;
        public string Argument;
        public Token(string command, string argument)
        {
            this.Command = command;
            this.Argument = argument;
        }
        public override string ToString()
        {
            return $"<'{Command}','{Argument}'>";
        }
    }
}
