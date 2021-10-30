using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class Tree_node
    {
        public string data;
        public double probably;
        public string code;
        private Tree_node leftChild;
        private Tree_node rightChild;
        private Tree_node parent;

        public Tree_node(string data, double probably)
        {
            leftChild = null;
            rightChild = null;
            parent = null;
            this.data = data;
            this.probably = probably;
        }

        public Tree_node Left
        {
            get
            {
                return leftChild;
            }
            set
            {
                leftChild = value;
            }
        }

        public Tree_node Right
        {
            get
            {
                return rightChild;
            }
            set
            {
                rightChild = value;
            }
        }

        public Tree_node Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
    }
}
