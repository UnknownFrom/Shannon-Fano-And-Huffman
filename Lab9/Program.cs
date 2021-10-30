using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class Program
    {
        static Dictionary<char, double> charAlphabet = new Dictionary<char, double>();
        static Dictionary<char, double> probably = new Dictionary<char, double>();
        static Dictionary<char, string> binTable = new Dictionary<char, string>();
        static void Main(string[] args)
        {
            string str = Console.ReadLine(); //ddddd bbb cccc aa
            createDictAlphabet(str);
            ShannonFano SF = new ShannonFano(probably);
            binTable = SF.BinTable;
            string binCode = coder(str);
            Console.WriteLine("Шеннон-Фано:\t " + binCode);
            Console.WriteLine("Результат:\t " + decoder(binCode));
            Console.WriteLine();
            Huffman H = new Huffman(probably);
            binTable = H.BinCode;
            binCode = coder(str);
            Console.WriteLine("Хаффмен:\t " + binCode);
            Console.WriteLine("Результат:\t " + decoder(binCode));

        }

        static string readFromFile(string path)
        {
            string result = "";
            using (StreamReader reader = new StreamReader(path))
            {
                result = reader.ReadToEnd();
                return result;
            }
        }

        static public string coder(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                result += binTable[str[i]];
            }
            return result;
        }
        static public string decoder(string str)
        {
            string result = "";
            while (str.Length != 0)
            {
                int i = 1;
                string code = str.Substring(0, i);
                while (!binTable.ContainsValue(code))
                {
                    i++;
                    if (i > str.Length)
                    {
                        return "error";
                    }
                    code = str.Substring(0, i);
                }
                for (int k = 0; k < binTable.Count; k++)
                {
                    if (code == binTable.ElementAt(k).Value)
                    {
                        result += binTable.ElementAt(k).Key;
                        str = str.Remove(0, i);
                        break;
                    }
                }
            }
            return result;
        }

        static void createDictAlphabet(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!charAlphabet.ContainsKey(str[i]))
                {
                    charAlphabet.Add(str[i], 1);
                }
                else
                {
                    charAlphabet[str[i]]++;
                }
            }
            foreach (var key in charAlphabet.Keys)
            {
                probably.Add(key, charAlphabet[key] / str.Length);
            }
            //sortProbably();
        }

        static void sortProbably()
        {
            probably = probably.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

    }
}
