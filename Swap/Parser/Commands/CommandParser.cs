using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Commands
{
    internal class CommandParser
    {
        List<ICommandParser> Parser;
        public CommandParser(List<ICommandParser> parsers)
        {
            this.Parser= parsers;
        }
        public AST.ProgramList Parse(List<Syntax.Token> code,int index,int length,int Line, Expressions.ExpressionParser expHandler)
        {
            AST.ProgramList list = new AST.ProgramList(Line);
            while (length != 0)
            {
                int lineLength;
                list.AddCommand(ParseLine(code, expHandler, index, out lineLength));
                index += lineLength;
                length -= lineLength;
            }
            return list;
        }
        public AST.ICommand ParseLine(List<Syntax.Token> code,Expressions.ExpressionParser expHandler, int index,out int length)
        {
            if (code[index].Command != "Int") throw new Exception("expected line to be numbered");
            int Line = Int32.Parse(code[index].Argument);
            if (code[index+1].Command != ".") throw new Exception($"'.' expected line: {Line}");
            if (code[index + 2].Command == "BraceOpen")
            {
                throw new NotImplementedException();
            }
            else
            {
                length = 0;
                while (code[index + length++].Command != ";") ;
                foreach(ICommandParser p in Parser)
                {
                    AST.ICommand result;
                    if (p.Parse(code,expHandler, index + 2, length - 3, Line, out result))
                    {
                        return result;
                    }
                }
                AST.IExpression exp = expHandler.Parse(code, index + 2, length - 3);
                if(exp is AST.IOptimizableExpression && (exp as AST.IOptimizableExpression).IsConstant())
                {
                    return new AST.Commands.StoreCommand(exp.Eval(null), Line);
                }
                else
                {
                    return new AST.Commands.StoreCommand(new AST.Values.VExpression(exp), Line);
                }
            }
        }
    }
}
