using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Values
{
    internal class VNode:IValue
    {
        LinkedListNode<ICommand> node;
        public VNode(LinkedListNode<ICommand> n)
        {
            this.node = n;
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
            n = this.node;
            return true;
        }
        public string Stringify()
        {
            return $"<Address:{node.Value.Line}>";
        }
    }
}
