using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewrite.AST.Expressions.Math
{
    internal static class ListNodeExtention
    {
        private static LinkedListNode<ICommand> GoNext(this LinkedListNode<ICommand> node,int i)
        {
            for(int j = 0; j < i; j++)
            {
                node=node.Next;
            }
            return node;
        }
        private static LinkedListNode<ICommand> GoPrev(this LinkedListNode<ICommand> node,int i)
        {
            for (int j = 0; j < i; j++)
            {
                node = node.Previous;
            }
            return node;
        }
        public static LinkedListNode<ICommand> Move(this LinkedListNode<ICommand> node,int i)
        {
            if (i < 0)
            {
                return node.GoPrev(-i);
            }
            else
            {
                return node.GoNext(i);
            }
        }

    }
}
