using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST
{
    internal interface IUnaryOperation
    {
        ExpressionContainer AExp { get; set; }
    }
}
