using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class Huffman
    {
        Dictionary<char, double> probably;
        Dictionary<char, string> binCode = new Dictionary<char, string>();
        List<Tree_node> binTree = new List<Tree_node>();
        static Tree_node HuffmanTree;
        public Huffman(Dictionary<char, double> probably)
        {
            if (probably.Count == 1)
            {
                binCode.Add(probably.ElementAt(0).Key, "0");
                return;
            }
            this.probably = probably; //beep boop beer!
            init();
            sortBinTree();
            createTree();
            writeCode(HuffmanTree);
            fillBinCode();
            return;
        }

        public Dictionary<char, string> BinCode
        {
            get
            {
                return binCode;
            }
        }

        public void fillBinCode()
        {
            foreach (var simbol in probably.Keys)
            {
                Tree_node node = HuffmanTree;
                string code = "";
                while (node.data != simbol.ToString())
                {
                    if (node.Left.data.IndexOf(simbol) != -1)
                    {
                        code += node.Left.code;
                        node = node.Left;
                    }
                    else
                    {
                        code += node.Right.code;
                        node = node.Right;
                    }
                }
                binCode.Add(simbol, code);
            }
        }

        public void writeCode(Tree_node node)
        {
            if (node.Left == null && node.Right == null)
            {
                return;
            }
            if (node.Left != null)
            {
                node.Left.code += "0";
                writeCode(node.Left);
            }
            if (node.Right != null)
            {
                node.Right.code += "1";
                writeCode(node.Right);
            }
        }

        public void init()
        {
            for (int i = 0; i < probably.Count; i++)
            {
                char simbolNode = probably.ElementAt(i).Key;
                double probablyNode = probably[simbolNode];
                Tree_node node = new Tree_node(simbolNode.ToString(), probablyNode);
                binTree.Add(node);
            }
        }

        public void createTree()
        {
            while (binTree.Count != 1)
            {
                string dataParent = binTree[0].data + binTree[1].data;
                double probablyParent = binTree[0].probably + binTree[1].probably;
                Tree_node parent = new Tree_node(dataParent, probablyParent);
                parent.Left = binTree[0];
                parent.Right = binTree[1];

                binTree[0].Parent = parent;
                binTree[1].Parent = parent;

                binTree.RemoveAt(0);
                binTree.RemoveAt(0);

                binTree.Insert(0, parent);

                sortBinTree();

                /*string simbolLeft = binTree[0].data;
                double probablyLeft = binTree[0].probably;
                Tree_node left = new Tree_node(simbolLeft, probablyLeft);

                string simbolRight = binTree[0].data;
                double probablyRight = binTree[0].probably;
                Tree_node right = new Tree_node(simbolRight, probablyRight);*/
            }
            HuffmanTree = binTree[0];
        }

        private void sortBinTree()
        {
            binTree = binTree.OrderBy(x => x.probably).ToList();
        }

        private void sortProbably()
        {
            probably = probably.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
