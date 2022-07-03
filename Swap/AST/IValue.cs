using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST
{
    internal interface IValue
    {
        bool GetInteger(out int i);
        bool GetString(out string s);
        bool GetNode(out LinkedListNode<ICommand> n);
        string Stringify();
    }
}
