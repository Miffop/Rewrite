using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands.StructureParsers
{
    internal class WhileAndIfStructParser:IStructureParser
    {
        public bool Parse(List<Syntax.Token> code,int index,out int length,int line,CommandParser cp, Expressions.ExpressionParser ep, out AST.ICommand com)
        {
            if (code[index].Command == "Word" &&
                (
                    code[index].Argument == "While" ||
                    code[index].Argument == "If"
                )
               )
            {
                if (code[index + 1].Command != "BraceOpen")
                {
                    throw new Exception("'(' expected");
                }
                int condExpLen = 1;
                int braceCounter = 1;
                while (braceCounter != 0)
                {
                    if (code[index + condExpLen + 1].Command == "BraceOpen")
                        braceCounter++;
                    if (code[index + condExpLen + 1].Command == "BraceClose")
                        braceCounter--;
                    condExpLen++;
                }
                AST.ExpressionContainer cond = ep.Parse(code, index + 1, condExpLen);
                if (code[index + condExpLen + 1].Command!="BraceOpen")
                {
                    throw new Exception("'{' expected");
                }
                int codeLen = 1;
                braceCounter=1;
                while (braceCounter != 0)
                {
                    if (code[index + condExpLen + 1 + codeLen].Command == "BraceOpen")
                        braceCounter++;
                    if (code[index + condExpLen + 1 + codeLen].Command == "BraceClose")
                        braceCounter--;
                    codeLen++;
                }
                AST.ProgramList result = null;
                switch (code[index].Argument)
                {
                    case "While":
                        result = new AST.Commands.Structures.WhileCommand(cond, line);
                        break;
                    case "If":
                        result = new AST.Commands.Structures.IFCommand(cond, line);
                        break;
                    default:
                        throw new Exception("This must be a bug");
                }
                cp.Parse(code, index + condExpLen + 2, codeLen - 2, ep, result);
                
                com = result;
                length = condExpLen + 1 + codeLen;
                return true;
            }

            length = 0;
            com = null;
            return false;

        }
    }
}
