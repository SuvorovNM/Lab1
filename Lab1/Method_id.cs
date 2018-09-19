using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Method_id:Ident
    {
        public Onewaylist owl;//A list for a way of transmission and a type of a certain parameter
        public Method_id(string n, Way k, Type t, Onewaylist list) : base(n, k, t)
        {
            owl = list;
        }
    }
}
