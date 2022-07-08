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
            return this.node==null;
        }
        public bool GetString(out string s)
        {
            s = "";
            return this.node == null;
        }
        public bool GetNode(out LinkedListNode<ICommand> n)
        {
            n = this.node;
            return n!=null;
        }
        public string Stringify()
        {
            return $"<Address:{node.Value.Line}>";
        }
    }
}
