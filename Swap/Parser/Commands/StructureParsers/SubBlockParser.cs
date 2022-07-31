using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Commands.StructureParsers
{
    internal class SubBlockParser:IStructureParser
    {
        public bool Parse(List<Syntax.Token> code, int index, out int length, int line, CommandParser cp, Expressions.ExpressionParser ep, out AST.ICommand com)
        {
            if(code[index].Command=="BraceOpen" && code[index].Argument == "{")
            {
                length = 1;
                int braceCounter = 1;
                while (braceCounter != 0)
                {
                    if (code[index + length].Command == "BraceOpen")
                    {
                        braceCounter++;
                    }
                    else if (code[index + length].Command == "BraceClose")
                    {
                        braceCounter--;
                    }
                    length++;
                }
                com = cp.Parse(code, index + 1, length - 2,ep,new AST.ProgramList(line));
                return true;
            }
            length = 0;
            com = null;
            return false;
        }
    }
}
