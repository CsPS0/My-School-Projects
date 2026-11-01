using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace taborok
{
    public record Tabor(int StartingMonth, int StartingDay, int EndingMonth, int EndingDay, string StudentsABC, string CampType)
    {
        public int Hossz()
        {
            return sorszam(EndingMonth, EndingDay) - sorszam(StartingMonth, StartingDay) + 1;
        }

        public static int sorszam(int month, int day)
        {
            int[] daysBefore = { 0, 0, 30, 61 };
            return daysBefore[month - 6] + (day - 16 + 1);
        }
    }
}