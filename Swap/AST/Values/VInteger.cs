using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Values
{
    internal class VInteger:IValue
    {
        int n;
        public VInteger(int i)
        {
            this.n = i;
        }
        public bool GetInteger(out int i)
        {
            i = this.n;
            return true;
        }
        public bool GetString(out string s)
        {
            s = this.n.ToString();
            return true;
        }
        public bool GetNode(out LinkedListNode<ICommand> node)
        {
            node = null;
            return false;
        }
        public bool GetExpression(out IExpression e)
        {
            e = null;
            return false;
        }
        public string Stringify()
        {
            return $"{n}";
        }
    }
}
