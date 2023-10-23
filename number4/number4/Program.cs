/*4.Реализуйте функцию для перевода десятичных дробей в систему счисления с основанием 𝑘 (𝑘
лежит в диапазоне [2..36]). Функция должна уметь выделять периодическую часть получившейся
дроби. Продемонстрируйте работу реализованной функции.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace number4
{
    class Program
    {
        static void Main(string[] args)
        {
            double num = 1.22222222222;
            int k = 16;

            int IntPart = (int)num;
            double FracPart = num - IntPart;
            string result = "";
            string resultFrac = "";

            while(IntPart>0)
            {
                int digit = IntPart % k;
                char c;
                if (digit < 10)
                {
                    c = (char)(digit + '0');
                }
                else
                {
                    c = (char)(digit - 10 + 'A');
                }
                IntPart /= k;
                result += c;
            }

            result += ".";

            int iterations = 9;
            int i = 0;
            while (FracPart > 0 && i < iterations)
            {
                FracPart *= k;
                int digit = (int)FracPart;

                char c;
                if (digit < 10)
                {
                    c = (char)(digit + '0');
                }
                else
                {
                    c = (char)(digit - 10 + 'A');
                }
                Console.WriteLine(FracPart);
                FracPart -= digit;
                Console.WriteLine(FracPart);
                resultFrac += c;
                i++;
            }
            Console.WriteLine(resultFrac);
            int periodStart = -1, periodLength = 0;
            for (int j = 0; j <= resultFrac.Length / 2; j++)
            {
                bool isPeriodic = true;
                for (int l = j; l < resultFrac.Length; l++)
                {
                    if (resultFrac[l] != resultFrac[l - j])
                    {
                        isPeriodic = false;
                        break;
                    }
                }
                if (isPeriodic == true)
                {
                    periodStart = j;
                    periodLength = j;
                }

                if (periodStart > 0)
                {
                    resultFrac = resultFrac.Substring(0, periodStart) + "(" + resultFrac.Substring(periodStart, periodLength) + ")";
                }
            }
            result += resultFrac;
            Console.WriteLine(result); // Output: "1.3C7B6FCA(81D5)"
        }
    }
}
