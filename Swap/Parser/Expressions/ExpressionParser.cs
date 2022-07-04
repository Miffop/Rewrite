using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Swap.AST;

namespace Swap.Parser.Expressions
{
    internal class ExpressionParser
    {
        List<IOperationParser> Operations;
        List<IValueParser> Values;
        public ExpressionParser(List<IOperationParser> operations,List<IValueParser> values)
        {
            this.Operations = operations;
            this.Values = values;
        }
        public IExpression Parse(List<Syntax.Token> code, int index, int length)
        {
            int priority = -1;
            int operationIndex = -1;
            IOperationParser operation = null;
            int braceCounter = 0;
            for(int i = 0; i < length; i++)
            {
                if (code[index + i].Command == "BraceOpen")
                {
                    braceCounter++;
                }
                else if (code[index + i].Command == "BraceClose")
                {
                    braceCounter--;
                }
                else if (code[index+i].Command=="Oper" && braceCounter == 0)
                {
                    foreach(IOperationParser currentOp in Operations)
                    {
                        int currentPriority;
                        if (currentOp.GetPriority(code[index + i].Argument, out currentPriority) && currentPriority >= priority)
                        {
                            operation = currentOp;
                            priority = currentPriority;
                            operationIndex = i;
                        }
                    }
                }
            }
            if (operation == null)
            {
                if (code[index].Command == "BraceOpen")
                {
                    if (code[index + length - 1].Command != "BraceClose")
                    {
                        throw new Exception("Closing Brace Expected");
                    }
                    return Parse(code, index + 1, length - 2);
                }
                else if (length == 0)
                {
                    return new AST.Expressions.ValueExpression(new AST.Values.VNull());
                }
                else
                {
                    foreach(IValueParser p in Values)
                    {
                        IExpression result;
                        if (p.Parse(code,index,length,out result))
                        {
                            return result;
                        }
                    }
                    throw new Exception("Unknown syntax");
                }
            }
            else
            {
                return operation.Parse(code[index + operationIndex].Argument, Parse(code, index, operationIndex), Parse(code, index + operationIndex + 1, length - operationIndex - 1));
            }
        }
    }
}
