using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics.Extension
{
    public static class CalcStatExtension
    {
        public static double GetPercentWinRate(this CalcStat calcStat)
        {
            return (calcStat.Count > 0 ? calcStat.WinRate : 0) * 100;
        }
    }
}
