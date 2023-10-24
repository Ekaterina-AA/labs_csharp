/*Реализуйте функции, вычисляющие значения чисел 𝑒, π, 𝑙𝑛 2 , 2, γ с заданной точностью.
Для каждой константы реализуйте три способа вычисления: как сумму ряда, как решение
специального уравнения, как значение предела.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace num2
{
    class Program
    {
        // 1
        public static double Lnx(double epsilon)
        {
            double x = 1.0;
            while (Math.Abs(Math.Log(x) - 1.0) > epsilon)
            {
                x *= Math.Exp(1 - Math.Log(x));
                if (x >= 0)
                {
                    return x;
                }
                    
            }    
            return double.NaN;
        }

        // 2
        public static double Cosx(double epsilon)
        {
            double x = Math.PI;

            while (Math.Abs(Math.Cos(x) + 1.0) > epsilon) 
            {
                x -= (Math.Cos(x) + 1.0) / Math.Sin(x); 
            }

            return x;
        }

        //3
        public static double Ex(double epsilon)
        {
            double x = 0.0;

            while (Math.Abs(Math.Exp(x) - 2.0) > epsilon) 
            {
                x -= (Math.Exp(x) - 2.0) / Math.Exp(x);
            }
            return x;
        }

        //4
        public static double X2(double epsilon)
        {
            double xn = 1.0;
            double xn_prev;
            do
            {
                xn_prev = xn;
                xn = xn_prev - (xn_prev * xn_prev - 2) / (2 * xn_prev); 
            } while (Math.Abs(xn - xn_prev) > epsilon);
            return xn;
        }

        static double Range1(double epsilon)
        {
            double sum = 0;
            int n = 0;
            double term = 1.0;
            while (term >= epsilon)
            { 
                term = 1.0 / Factorial(n); 
                sum += term; 
                n++;
            }
            return sum;
        }

        static int Factorial(int n)
        { 
            int result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }


        static void Main(string[] args)
        {
            double.TryParse(args[0], out double eps);
            // 1
            double x = Lnx(eps);
            if (x == double.NaN)
            {
                Console.WriteLine("wrong");
            }
            else
            {
                Console.WriteLine("Решение уравнения ln x = 1: {0}", x);
            }

            //2
            x = Cosx(eps);
            Console.WriteLine("Решение уравнения cos x = -1: {0}", x);
            

            //3
            x = Ex(eps);
            Console.WriteLine("Решение уравнения e^x = 2: {0}", x);

            //4
            x = X2(eps);
            Console.WriteLine("Решение уравнения x^2 = 2: {0}", x);


            //
            x = Range1(eps);
            Console.WriteLine("Сумма ряда: {0}", x);
          



















        /* try
         {
             if (args.Length != 1)
             {
                 throw new Exception("Please provide an epsilon ONE value as a command line argument.");
             }

             if (!Double.TryParse(args[0], out double epsilon))
             {
                 throw new Exception("The provided argument is not a valid decimal number.");
             }

             Console.WriteLine("Calculating e using limit with provided epsilon: {0}", epsilon);

             double lim_e = ELim(epsilon, 10000);

             double sum_e = Esum(epsilon);

             double sum_PI = PIsum(epsilon);

             Console.WriteLine("Calculated value of e LIMIT: {0}", lim_e);

             Console.WriteLine("Calculated value of e SUM: {0}", sum_e);

             Console.WriteLine("Calculated value of PI SUM: {0}", sum_PI);

         }
         catch (Exception e)
         {
             Console.WriteLine($"Error: {e.Message}");
         }
     }

     public static double ELim(double epsilon, int maxIterations)
     {
         double ePrevious = 0;
         double eCurrent = 1;

         for (int n = 1; n <= maxIterations && Math.Abs(eCurrent - ePrevious) > epsilon; n++)
         {
             ePrevious = eCurrent;
             eCurrent = Math.Pow(1 + 1.0 / n, n);
         }

         return eCurrent;
     }
     public static double Esum(double epsilon)
     {
         double term = 1; double result = 1;
         ulong n = 1;

         while (term > epsilon)
         {
             term = 1f / n;
             result += 1f / n;
             n *= ++n;
         }

         return result;
     }



     public static double PIsum(double epsilon)
     {
         double pi = 0;
         int n = 0;
         double term = 1;

         while (Math.Abs(term) >= epsilon)
         {
             term = 1.0 / (2 * n + 1);
             pi += term;
             n++;
         }

         return 4 * pi;*/
    }
    }

}