using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST
{
    internal class ProgramList:ICommand
    {

        
        protected LinkedList<ICommand> list;

        public ProgramList(int ln)
        {
            list = new LinkedList<ICommand>();
            this.Line = ln;
        }

        protected override LinkedListNode<ICommand> Exec(Context c)
        {
            c = new Context(c.Root, this);
            LinkedListNode<ICommand> CurrentNode = list.First;
            while (CurrentNode != null)
            {
                CurrentNode=CurrentNode.Value.Execute(c);
            }
            if (this.Parent != null)
            {
                return this.Parent.Next;
            }
            else
            {
                return null;
            }
        }
        public LinkedListNode<ICommand> AddCommand(ICommand n)
        {
            int i = 0;
            LinkedListNode<ICommand> CurrentNode = list.First;
            if (CurrentNode == null)
            {
                return n.Parent=list.AddFirst(n);
            }
            while (CurrentNode != null)
            {
                if (CurrentNode.Value.Line < n.Line)
                {
                    CurrentNode = CurrentNode.Next;
                    i++;
                }
                else if (CurrentNode.Value.Line == n.Line)
                {
                    throw new Exception("Cannot have two lines with the same number");
                }
                else
                {
                    return n.Parent=list.AddBefore(CurrentNode, n);
                }
            }
            return n.Parent=list.AddLast(n);
            
        }
        public LinkedListNode<ICommand> Find(int i)
        {
            LinkedListNode<ICommand> CurrentNode = list.First;
            while(CurrentNode != null)
            {
                if (CurrentNode.Value.Line == i)
                {
                    return CurrentNode;
                }
                if (CurrentNode.Value.Line > i)
                {
                    return AddCommand(new Commands.NoCommand(i));
                }
                CurrentNode = CurrentNode.Next;
            }
            return AddCommand(new Commands.NoCommand(i));
        }
        public override string Stringify()
        {
            return list.Aggregate<ICommand, string>("{\n", (sum, com) => sum+=$"{com.Line}.{com.Stringify()}\n")+"}";
        }

    }
}
