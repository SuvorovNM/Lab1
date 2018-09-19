using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Const_id:Ident
    {
        public object value;//Value of const
        public Const_id(string n, Way k, Type t, object val) : base(n, k, t)
        {
            value = val;
        }
    }
}
