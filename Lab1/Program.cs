using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree=null;
            Regex patternVar = new Regex("^(int|bool|string|float|char)[ ][A-Za-z0-9_]+?$");
            Regex patternClass = new Regex("^class[ ][A-Za-z0-9_]+?$");
            Regex patternConst = new Regex("^const[ ](int|bool|string|float|char)[ ][A-Za-z0-9_]+?[ ][=][ ][A-Za-z0-9_]+?$");
            Regex patternMethod = new Regex("^(int|bool|string|float|char)[ ][A-Za-z0-9_]+?[(][A-Za-z0-9_, ]+?[)]$");
            string exemp;
            bool root = true;
            StreamReader str = new StreamReader("input.txt");
            while (!str.EndOfStream)
            {
                exemp = str.ReadLine().ToLower();
                exemp=exemp.Substring(0, exemp.Length-1);
                if (patternVar.IsMatch(exemp))
                //Добавление переменной
                {
                    AddVar(ref tree, exemp, ref root);
                }
                else if (patternClass.IsMatch(exemp))
                //Добавление класса
                {
                    string[] words = exemp.Split(' ');
                    Class_id class_t = new Class_id(words[1], Way.Classes, Type.class_type);
                    if (root)
                    {
                        tree = new Tree(class_t);
                        root = false;
                    }
                    else tree.Addtree(class_t);
                }
                else if (patternConst.IsMatch(exemp))
                //Добавление константы
                {
                    AddConst(ref tree, exemp, ref root);
                }
                else
                {
                    //Добавление метода
                    AddMethod(ref tree, exemp, ref root);
                }
            }//stop

        }

        private static void AddMethod(ref Tree tree, string exemp, ref bool root)
        //Adding a method
        {
            //name[] - type and name of the method
            string[] name = exemp.Substring(0, exemp.IndexOf('(')).Split(' ');
            //t1 - type of the method
            Type t1 = new Type();
            t1 = CaseOfTypes(name, t1, 0);
            Onewaylist list=null;
            string str = exemp.Substring(exemp.IndexOf('(') + 1, exemp.IndexOf(')') - (exemp.IndexOf('(') + 1)).Replace(" ", "");
            if (str != "")
            {
                //par - array of parameters of the method (e.g. "ref char x2")
                string[] par = exemp.Substring(exemp.IndexOf('(') + 1, exemp.IndexOf(')') - (exemp.IndexOf('(') + 1)).Split(',');
                //variables - array of [way of transmission,type, name] or [type, name]
                string[] variables;
                variables = par[0].Split(' ');
                //p - way of transmission
                param p = new param();
                //telem - type of the parameter
                Type telem = new Type();
                //if there are only 2 words in variables:
                if (variables[0] != "ref" && variables[0] != "out")
                {
                    p = param.param_val;
                    telem = CaseOfTypes(variables, telem, 0);
                }
                //if there are 3 words in variables:
                else
                {
                    telem = CaseOfTypes(variables, telem, 1);
                    if (variables[0] == "ref")
                        p = param.param_ref;
                    else p = param.param_out;
                }
                //list - unidirectional list with 3 fields: way of transmission,type, next_element
                list = new Onewaylist(telem, p);
                //beg - link to list, to go through it and add elements
                Onewaylist beg = list;
                //Until all parameters are executed:
                for (int i = 1; i < par.Length; i++)
                {
                    //delete the first ' ' in par[i][0] if it exists
                    if (par[i][0] == ' ')
                        par[i] = par[i].Substring(1);
                    //variables - array of [way of transmission,type, name] or [type, name]
                    variables = par[i].Split(' ');
                    //if there are only 2 words in variables:
                    if (variables[0] != "ref" && variables[0] != "out")
                    {
                        p = param.param_val;
                        telem = CaseOfTypes(variables, telem, 0);
                    }
                    //if there are 3 words in variables:
                    else
                    {
                        telem = CaseOfTypes(variables, telem, 1);
                        if (variables[0] == "ref")
                            p = param.param_ref;
                        else p = param.param_out;
                    }
                    //Adding element to the list and putting a pointer on the next element
                    beg.next = new Onewaylist(telem, p);
                    beg = beg.next;
                }
            }
            //Adding identificator to the tree
            Method_id met_t = new Method_id(name[1], Way.Methods, t1, list);
            //If it is the first element:
            if (root)
            {
                tree = new Tree(met_t);
                root = false;
            }
            //If some elements already exist in the tree:
            else tree.Addtree(met_t);
        }

        private static Type CaseOfTypes(string[] variables, Type telem, int i)
        //Method for choosing type based on what is in variables[i]
        {
            switch (variables[i])
            {
                case "int":
                    telem = Type.int_type;
                    break;
                case "float":
                    telem = Type.float_type;
                    break;
                case "string":
                    telem = Type.string_type;
                    break;
                case "bool":
                    telem = Type.bool_type;
                    break;
                case "char":
                    telem = Type.char_type;
                    break;
            }

            return telem;
        }

        private static void AddConst(ref Tree tree, string exemp, ref bool root)
        //Adding a constant
        {
            string[] words = exemp.Split(' ');
            //t1 - type of const
            Type t1 = new Type();
            //obj - value of const
            object obj = new object();
            //Obtainment of types and values
            switch (words[1])
            {
                case "int":
                    t1 = Type.int_type;
                    obj = Int64.Parse(words[words.Length - 1]);
                    break;
                case "float":
                    t1 = Type.float_type;
                    obj = float.Parse(words[words.Length - 1]);
                    break;
                case "string":
                    t1 = Type.string_type;
                    obj = words[words.Length - 1];
                    break;
                case "bool":
                    t1 = Type.bool_type;
                    obj = Boolean.Parse(words[words.Length - 1]);
                    break;
                case "char":
                    t1 = Type.char_type;
                    obj = Char.Parse(words[words.Length - 1]);
                    break;
            }
            Const_id const_t = new Const_id(words[words.Length - 3], Way.Consts, t1, obj);
            //If it is the first element:
            if (root)
            {
                tree = new Tree(const_t);
                root = false;
            }
            //If some elements already exist in the tree:
            else tree.Addtree(const_t);
        }

        private static void AddVar(ref Tree tree, string exemp, ref bool root)
        //Adding a variable
        {
            Type t1 = new Type();
            string[] words = exemp.Split(' ');
            t1 = CaseOfTypes(words, t1, 0);
            Var_id var_t = new Var_id(words[1], Way.Vars, t1);
            //If it is the first element:
            if (root)
            {
                tree = new Tree(var_t);
                root = false;
            }
            //If some elements already exist in the tree:
            else tree.Addtree(var_t);
        }
    }
}
