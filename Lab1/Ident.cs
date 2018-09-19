using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public enum Way{Classes,Consts,Vars,Methods }
    public enum Type { int_type, float_type, bool_type, char_type, string_type, class_type };
    abstract class Ident:IComparable
    {
        public int Hashcode;//Hashcode of an identificator based on it's name
        public Way wayofcreating;//Way of usage of an identificator (Classes,Consts,Vars,Methods)
        public Type typeofid;//Type of an identificator (int_type, float_type, bool_type, char_type, string_type, class_type)
        public string name;//name of an identificator
        public Ident(string n,Way k, Type t)
        {
            name = n;
            wayofcreating = k;
            typeofid = t;
            Hashcode = name.GetHashCode();
        }
        public int CompareTo(object obj)
        {
            Ident ob = obj as Ident;
            if (ob != null)
            {
                double str = Hashcode;
                double str1 = ob.Hashcode;
                return str.CompareTo(str1);
            }
            return 0;
        }
    }
}
