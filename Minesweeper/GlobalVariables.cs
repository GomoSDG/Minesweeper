using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    static class GlobalVariables
    {
        public static Random rndm;
        static GlobalVariables()
        {
            rndm = new Random(DateTime.Now.Millisecond);
        }
    }
}
