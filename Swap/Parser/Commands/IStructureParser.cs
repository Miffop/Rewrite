using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands
{
    internal interface IStructureParser
    {
        bool Parse(List<Syntax.Token> code,int index,out int length,int line,CommandParser cp, Expressions.ExpressionParser ep, out AST.ICommand com);
    }
}
