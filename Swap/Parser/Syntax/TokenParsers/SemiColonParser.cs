﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.Parser.Syntax.TokenParsers
{
    internal class SemiColonParser:ITokenParser
    {
        public bool GetLength(string code,int index,out int length)
        {
            if (code[index] == ':' && code[index + 1] == ':')
            {
                length = 2;
                return true;
            }
            length = 1;
            return 
                code[index] == ';' ||
                code[index] == '.' ||
                code[index] == ',' ||
                code[index] == ':';
        }
        public Token Parse(string code,int index)
        {
            switch (code[index])
            {
                case ';':
                    return new Token(";", "");
                case '.':
                    return new Token(".", "");
                case ',':
                    return new Token(",", "");
                case ':':
                    if (code[index + 1] == ':')
                    {
                        return new Token("Oper", "::");
                    }
                    return new Token("Oper", ":");
                default:
                    throw new Exception();
            }
        }
    }
}
