﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swap.AST.Expressions.Math
{
    internal class MulExpression:IExpression
    {
        IExpression AExp, BExp;
        public MulExpression(IExpression a,IExpression b)
        {
            this.AExp = a;
            this.BExp = b;
        }
        public IValue Eval(Context c)
        {
            IValue A = AExp.Eval(c);
            IValue B = BExp.Eval(c);

            int iA, iB;
            string sA,sB;
            if(A.GetInteger(out iA) && B.GetInteger(out iB))
            {
                return new Values.VInteger(iA * iB);
            }
            else if (A.GetString(out sA) && B.GetInteger(out iB))
            {
                string res = "";
                for (int i = 0; i < iB; i++)
                {
                    res += sA;
                }
                return new Values.VString(res);
            }
            else if (A.GetInteger(out iA) && B.GetString(out sB))
            {
                string res = "";
                for (int i = 0; i < iA; i++)
                {
                    res += sB;
                }
                return new Values.VString(res);
            }
            else
            {
                throw new Exception($"Cannot perform: {this.Stringify()}");
            }
        }
        public string Stringify()
        {
            return $"({AExp.Stringify()} * {BExp.Stringify()})";
        }
    }
}
