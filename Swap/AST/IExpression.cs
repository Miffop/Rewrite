using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST
{
    internal interface IExpression
    {
        IValue Eval(Context c);
        string Stringify();
    }
}
