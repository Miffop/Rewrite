using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Commands.CommandParsers
{
    internal class AssignCommandParser:ICommandParser
    {
        public bool Parse(List<Syntax.Token> code, Expressions.ExpressionParser expHandler, int index, int length, int line, out AST.ICommand com)
        {
            int braceCounter = 0;
            int len = 0;
            while(len<length)
            {
                if (code[index + len].Command == "BraceOpen")
                {
                    braceCounter++;
                }
                if (code[index + len].Command == "BraceClose")
                {
                    braceCounter--;
                }
                if(code[index+len].Command == "=")
                {
                    AST.ExpressionContainer AExp = expHandler.Parse(code, index, len);
                    AST.ExpressionContainer BExp = expHandler.Parse(code, index + len + 1, length - len - 1);
                    com = new AST.Commands.SetCommand(AExp, BExp, line);
                    return true;
                }
                len++;
            }
            com = null;
            return false;
        }
    }
}
