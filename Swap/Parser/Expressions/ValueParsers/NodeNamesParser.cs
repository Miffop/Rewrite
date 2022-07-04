﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Expressions.ValueParsers
{
    internal class NodeNamesParser:IValueParser
    {
        public bool Parse(List<Syntax.Token> code,int index,int length,out AST.IExpression val)
        {
            if (length == 1 && code[index].Command=="Word")
            {
                switch (code[index].Argument)
                {
                    case "this":
                        val = new AST.Expressions.ThisBlockExpression();
                        return true;
                    case "current":
                        val=new AST.Expressions.CurrentNodeExpression();
                        return true;
                    case "root":
                        val = new AST.Expressions.RootNodeExpression();
                        return true;
                }
            }
            val = null;
            return false;
        }
    }
}