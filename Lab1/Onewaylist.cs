using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    enum param { param_val,param_ref,param_out}
    class Onewaylist
    {
        public Type t;//type
        public param p;//way of transmission
        public Onewaylist next;//next element
        public Onewaylist(Type tp,param par)
        {
            t = tp;
            p = par;
            next = null;
        }
    }
}
