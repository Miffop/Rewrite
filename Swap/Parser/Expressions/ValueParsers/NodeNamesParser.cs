using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.ValueParsers
{
    internal class NodeNamesParser:IValueParser
    {
        public bool Parse(List<Syntax.Token> code,int index,int length, Expressions.ExpressionParser ep, out AST.IExpression val)
        {
            if (length == 1 && code[index].Command=="Word")
            {
                switch (code[index].Argument)
                {
                    case "this":
                        val = new AST.Expressions.ThisBlockExpression(null);
                        return true;
                    case "current":
                        val=new AST.Expressions.CurrentNodeExpression(null);
                        return true;
                    case "root":
                        val = new AST.Expressions.RootNodeExpression(null);
                        return true;
                }
            }
            val = null;
            return false;
        }
    }
}
