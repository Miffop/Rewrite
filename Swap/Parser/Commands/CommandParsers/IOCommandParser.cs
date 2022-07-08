using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands.CommandParsers
{
    internal class IOCommandParser:ICommandParser
    {
        public bool Parse(List<Syntax.Token> code, Expressions.ExpressionParser expHandler, int index, int length, int line, out AST.ICommand com)
        {
            if (code[index].Command=="Word")
            {
                if (
                    code[index].Argument == "Print" || 
                    code[index].Argument=="Input"
                    )
                {
                    if (code[index + 1].Command != "BraceOpen")
                    {
                        throw new Exception("'(' expected");
                    }
                    if (code[index + length-1].Command != "BraceClose")
                    {
                        throw new Exception("')' expected");
                    }
                    AST.IExpression lineExp = expHandler.Parse(code, index + 2, length - 3);
                    switch (code[index].Argument)
                    {
                        case "Print":
                            com = new AST.Commands.PrintCommand(lineExp, line);
                            break;
                        case "Input":
                            com = new AST.Commands.InputCommand(lineExp, line);
                            break;
                        default:
                            throw new Exception("It is a bug!!!");
                    }
                    return true;
                }
            }
            com = null;
            return false;
        }
    }
}
