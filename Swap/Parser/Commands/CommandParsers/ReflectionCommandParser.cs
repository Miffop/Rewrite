using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands.CommandParsers
{
    internal class ReflectionCommandParser:ICommandParser
    {
        public bool Parse(List<Syntax.Token> code, Expressions.ExpressionParser ep, int index, int length, int line, out AST.ICommand com)
        {
            if (code[index].Command == "Word")
            {
                if (
                    code[index].Argument == "Swap" ||
                    code[index].Argument=="Set"
                    )
                {
                    if (code[index + 1].Command != "BraceOpen")
                    {
                        throw new Exception("'(' expected");
                    }
                    if (code[index + length - 1].Command != "BraceClose")
                    {
                        throw new Exception("')' expected");
                    }
                    int braceCounter = 0;
                    int len1 = 2;
                    while (code[index + len1].Command != "," || braceCounter!=0)
                    {
                        if (code[index + len1].Command == "BraceOpen")
                        {
                            braceCounter++;
                        }
                        if (code[index + len1].Command == "BraceClose")
                        {
                            braceCounter--;
                        }
                        len1++;
                    }
                    AST.IExpression A = ep.Parse(code, index + 2, len1 - 2);
                    AST.IExpression B = ep.Parse(code, index + len1 + 1, length - len1 - 2);
                    switch (code[index].Argument)
                    {
                        case "Swap":
                            com = new AST.Commands.SwapCommand(A, B, line);
                            break;
                        case "Set":
                            com = new AST.Commands.SetCommand(A, B, line);
                            break;
                        default:
                            throw new Exception("This has to be a bug");
                    }
                    return true;

                }
            }
            com = null;
            return false;
        }
    }
}
