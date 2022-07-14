using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Values
{
    internal class VNull:IValue
    {
        public bool GetInteger(out int i)
        {
            i = 0;
            return true;
        }
        public bool GetString(out string s)
        {
            s = "";
            return true;
        }
        public bool GetNode(out LinkedListNode<ICommand> n)
        {
            n = null;
            return false;
        }
        public bool GetExpression(out IExpression e)
        {
            e = null;
            return false;
        }
        public string Stringify()
        {
            return "null";
        }
    }
}
