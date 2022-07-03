﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions
{
    internal class CurrentNodeExpression:IExpression
    {
        public IValue Eval(Context c)
        {
            return new Values.VNode(c.Current.Find(c.ExecutingLine));
        }
        public string Stringify()
        {
            return "current";
        }
    }
}
