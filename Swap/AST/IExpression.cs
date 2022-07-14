﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST
{
    internal interface IExpression
    {
        ExpressionContainer Parent { get; set; }
        IValue Eval(Context c);
        string Stringify();
    }
}
