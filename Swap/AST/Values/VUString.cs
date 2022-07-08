﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Values
{
    internal class VUString:IValue
    {
        string str;
        public VUString(string s)
        {
            this.str = s;
        }
        public bool GetInteger(out int i)
        {
            i = 0;
            return false;
        }
        public bool GetString(out string s)
        {
            s = this.str;
            return true;
        }
        public bool GetNode(out LinkedListNode<ICommand> n)
        {
            n = null;
            return false;
        }
        public string Stringify()
        {
            return $"`{str.Replace("\\", "\\b").Replace("\"", "\\q").Replace("\n", "\\n")}`";
        }
    }
}
