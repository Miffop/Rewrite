﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST
{
    internal interface IUnaryOperation
    {
        ExpressionContainer AExp { get; set; }
    }
}
