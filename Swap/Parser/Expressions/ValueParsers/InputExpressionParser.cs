using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Expressions.ValueParsers
{
    internal class InputExpressionParser:IValueParser
    {
        public bool Parse(List<Syntax.Token> code,int index,int length,ExpressionParser ep,out AST.IExpression exp)
        {
            if(code[index].Command=="Word" && length==1)
            {
                switch (code[index].Argument)
                {
                    case "input":
                        exp = new AST.Expressions.InputExpression(null);
                        return true;
                }
            }
            exp=null;
            return false;
        }
    }
}
