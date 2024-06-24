using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GemsOfWarsWARSTAT.TextAnalysis
{
    public static class CompareTextHelper
    {
        public static bool IsRussian(char letter)
        {
            // Шаблон для поиска русских букв в тексте
            string pattern = @"\p{IsCyrillic}";

            // Проверяем, содержит ли текст русские буквы
            bool containsRussianLetters = Regex.IsMatch(new string(letter, 1), pattern, RegexOptions.IgnoreCase);

            return containsRussianLetters;
        }

        public static int LevenshteinDistance(string s, string t)
        {
            int m = s.Length;
            int n = t.Length;
            int[,] d = new int[m + 1, n + 1];

            if (m == 0)
                return n;

            if (n == 0)
                return m;

            for (int i = 0; i <= m; i++)
                d[i, 0] = i;

            for (int j = 0; j <= n; j++)
                d[0, j] = j;

            for (int j = 1; j <= n; j++)
            {
                for (int i = 1; i <= m; i++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[m, n];
        }

        public static T FindBestItem<T>(string etalone, ICollection<T> items, Func<T, string> findTest) where T : class
        {
            var minDist = int.MaxValue;
            T bestItem = null;
            foreach (var item in items)
            {
                var curDist = LevenshteinDistance(etalone, findTest(item));
                if (curDist == 0)
                {
                    return item;
                }
                if (curDist < minDist && curDist <= 2)
                {
                    bestItem = item;
                }
            }
            return bestItem;
        }
    }
}
