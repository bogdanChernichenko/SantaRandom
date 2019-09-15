using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaRandom
{
    public static class StaticDataStorage
    {
        private static List<string> pairList = new List<string>();

        public static List<string> PairsList
        {
            get
            {
                return pairList;
            }
            set
            {
                pairList = value;
            }
        }
    }
}
