using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands
{
    internal interface ICommandParser
    {
        bool Parse(List<Syntax.Token> code, Expressions.ExpressionParser expHandler, int index, int length, int line, out AST.ICommand com);
    }
}
