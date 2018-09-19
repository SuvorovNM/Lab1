using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Tree
    {
        Ident id;//identificator
        Tree left, right;//right and left subtrees
        public Tree(Ident identificator = null)
        {
            id = identificator;
            left = null;
            right = null;
        }
        public void Addtree(Ident node)
        //Adding a node in the tree by comparing indexes of nodes
        {
            Tree t = this;
            if (node.Hashcode < id.Hashcode)
                if (left != null)
                    t.left.Addtree(node);
                else t.left = new Tree(node);
            else
            {
                if (right != null)
                    t.right.Addtree(node);
                else t.right = new Tree(node);
            }
        }
    }
}
