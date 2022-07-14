using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Commands.CommandParsers
{
    internal class EmptyCommandParser:ICommandParser
    {
        public bool Parse(List<Syntax.Token> code,Expressions.ExpressionParser ep,int index,int length,int line,out AST.ICommand com)
        {
            if (code[index].Command == "Word" && length== 3 && code[index + 1].Command == "BraceOpen" && code[index+2].Command=="BraceClose")
            {
                switch (code[index].Argument)
                {
                    case "Nope":
                        com = new AST.Commands.NoCommand(line);
                        return true;
                    case "Halt":
                        com = new AST.Commands.HaltCommand(line);
                        return true;
                }
            }
            com = null;
            return false;
        }

    }
}
