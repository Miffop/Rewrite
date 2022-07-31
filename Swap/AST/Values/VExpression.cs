using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Values
{
    internal class VExpression:IValue
    {
        IExpression exp;
        public VExpression(IExpression e)
        {
            this.exp = e;
        }
        public bool GetInteger(out int i)
        {
            i = 0;
            return false;
        }
        public bool GetString(out string s)
        {
            s = null;
            return false;
        }
        public bool GetNode(out LinkedListNode<ICommand> n)
        {
            n = null;
            return false;
        }
        public bool GetExpression(out IExpression e)
        {
            e = this.exp;
            return true;
        }
        public string Stringify()
        {
            return $"[{exp.Stringify()}]";
        }
    }
}
