using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class ShannonFano
    {
        Dictionary<char, double> probably;
        Dictionary<char, string> binTable = new Dictionary<char, string>();
        public ShannonFano(Dictionary<char, double> probably)
        {
            if (probably.Count == 1)
            {
                binTable.Add(probably.ElementAt(0).Key, "0");
                return;
            }
            this.probably = probably;
            sortProbably();
            createBinTable();
            return;
        }

        private void sortProbably()
        {
            probably = probably.OrderByDescending(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<char, string> BinTable
        {
            get
            {
                return binTable;
            }
            set
            {
                binTable = value;
            }
        }

        public void createBinTable()
        {
            for (int i = 0; i < probably.Count; i++)
            {
                binTable.Add(probably.ElementAt(i).Key, "");
            }
            fillBinTable(0, probably.Count, 1.0);
        }

        public void fillBinTable(int start, int end, double allProb)
        {
            if ((end - start) == 1)
            {
                return;
            }

            KeyValuePair<int, double> median = findMedian(start, end, allProb);
            fillNumTable(start, median.Key, "0");
            fillNumTable(median.Key, end, "1");
            fillBinTable(start, median.Key, median.Value);
            fillBinTable(median.Key, end, (allProb - median.Value));

        }

        public void fillNumTable(int start, int end, string num)
        {
            for (int i = start; i < end; i++)
            {
                char key = binTable.ElementAt(i).Key;
                binTable[key] = binTable[key] + num;
            }
        }

        /// <summary>
        /// Возвращает индекс медианы и сумму значений до неё
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="allProb"></param>
        /// <returns></returns>
        public KeyValuePair<int, double> findMedian(int start, int end, double allProb)
        {
            double sum = 0;
            int i = start;
            while (Math.Abs(sum - (allProb - sum)) >= Math.Abs((sum + probably.ElementAt(i).Value) - (allProb - (sum + probably.ElementAt(i).Value))) && i < end)
            {
                sum += probably.ElementAt(i).Value;
                i++;
            }
            KeyValuePair<int, double> result = new KeyValuePair<int, double>(i, sum);
            return result;
        }
    }
}
