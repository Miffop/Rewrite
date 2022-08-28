using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.Parser.Commands
{
    internal class CommandParser
    {
        List<ICommandParser> ComParser;
        List<IStructureParser> StructParser;

        public CommandParser(List<ICommandParser> parsers,List<IStructureParser> structs)
        {
            this.ComParser= parsers;
            this.StructParser = structs;
        }
        public AST.ProgramList Parse(List<Syntax.Token> code,int index,int length, Expressions.ExpressionParser expHandler,AST.ProgramList list)
        {
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
            
            //Struct parser
            {
                foreach(IStructureParser sp in StructParser)
                {
                    if (sp.Parse(code, index+2, out length,Line, this, expHandler, out AST.ICommand result))
                    {
                        length+=2;
                        return result;
                    }
                }
            }
            //Command parser
            {
                length = 0;
                while (code[index + length++].Command != ";") ;
                foreach(ICommandParser p in ComParser)
                {
                    AST.ICommand result;
                    if (p.Parse(code,expHandler, index + 2, length - 3, Line, out result))
                    {
                        return result;
                    }
                }
                //If not a command
                AST.ExpressionContainer exp = expHandler.Parse(code, index + 2, length - 3);
                if(exp.Expression is AST.IOptimizableExpression && (exp.Expression as AST.IOptimizableExpression).IsConstant())
                {
                    return new AST.Commands.StoreCommand(exp.Expression.Eval(null), Line);
                }
                else
                {
                    throw new Exception($"Unknown syntax line:{Line}");
                    //return new AST.Commands.StoreCommand(new AST.Values.VExpression(exp.Expression), Line);
                }
            }
        }
    }
}
