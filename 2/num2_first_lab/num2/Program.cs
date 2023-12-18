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
        //1.1

        static double Lim1(double epsilon)
        {
            double prev = 0, cur = 1;
            for (int counter = 1; counter <= int.MaxValue && Math.Abs(cur - prev) > epsilon; counter++)
            {
                prev = cur;
                cur = Math.Pow(1 + 1.0 / counter, counter);
            }
            return cur;
        }

        //1.2

        static double Lim2(double epsilon)
        {
            double prev = 0, cur = 1;
            for (int counter = 1; counter <= int.MaxValue && Math.Abs(cur - prev) > epsilon; counter++)
            {
                prev = cur;
                double holder1 = Math.Pow(2, counter) * Factorial(counter);
                double upperPart = Math.Pow(holder1, 4);
                double holder2 = Factorial(counter * 2);
                double lowerPart = counter * Math.Pow(holder2, 2);
                if (lowerPart > int.MaxValue)
                    break;
                cur = upperPart / lowerPart;

            }
            return cur;
        }

        //1.3
        static double Lim3(double epsilon)
        {
            double prev = 0, cur = 2;
            for (int counter = 1; counter <= int.MaxValue && Math.Abs(cur - prev) > epsilon; counter++)
            {
                prev = cur;
                cur = counter * (Math.Pow(2, 1.0 / counter) - 1);
            }
            return cur;
        }

        //1.4
        static double Lim4(double epsilon)
        {
            double prev = 0, cur = -0.5;
            for (int counter = 1; counter <= int.MaxValue && Math.Abs(cur - prev) > epsilon; counter++)
            {
                prev = cur;
                cur = prev - (Math.Pow(prev, 2) / 2) + 1;
            }
            return cur;
        }

        //1.5
        static double Lim5(double epsilon)
        {
            double sum = 0;
            double term;
            double prev = 0, cur = -0.5;
            for (int counter1 = 1; counter1 <= int.MaxValue && Math.Abs(cur - prev) > epsilon; counter1++)
            {
                if (cur != 0)
                    prev = cur;
                int counter2 = 1;
                while (counter2 < counter1)
                {
                    double holder1 = Factorial(counter1) / (Factorial(counter2) * Factorial(counter1 - counter2));
                    if (holder1 <= 0)
                        break;
                    double holder2 = Math.Pow(-1, counter2) / counter2;
                    double holder3 = Math.Log(Factorial(counter2));
                    if (holder3 is double.NaN)
                        break;
                    term = holder1 * holder2 * holder3;
                    sum += term;
                    counter2++;
                }

                cur = sum;
            }
            return cur;
        }


        // 2.1
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

        //2.2
        static double Range2(double epsilon)
        {
            double sum = 0;
            int n = 1;
            double term = 1.0;
            while (Math.Abs(term) > epsilon)
            {
                term = (double)Math.Pow(-1, n - 1) / (2 * n - 1);
                sum += term;
                n++;
            }
            return sum * 4;
        }

        //2.3
        static double Range3(double epsilon)
        {
            double sum = 0;
            int n = 1;
            double term = 1.0;
            while (Math.Abs(term) > epsilon)
            {
                term = (double)Math.Pow(-1, n - 1) / n;
                sum += term;
                n++;
            }
            return sum;
        }

        //2.4
        static double Range4(double epsilon)
        {
            double n;
            double multiply = Math.Pow(2, 0.25);
            int k = 3;
            double term;

            while ((multiply - 1) / 1000 > epsilon)
            {
                multiply = 1;

                n = (double)Math.Pow(2, -k);
                term = (double)Math.Pow(2, n);

                multiply *= term;
                k++;
            }
            return multiply;
        }

        //2.5
        static double Range5(double epsilon)
        {
            double sum = 0;
            //          int counter = 2;
            //          double term = 1.0;
            //          while (Math.Abs(term) > epsilon)
            //          {
            //              double holder1 = Math.Round((double)Math.Sqrt(counter), MidpointRounding.ToZero);
            //              term = (1.0 / Math.Pow(holder1, 2)) - ( 1.0 / counter);
            //              sum += term;
            //              counter++;
            //              Console.WriteLine(holder1);
            //          }
            return -Math.Pow(Math.PI, 2) / 6 + sum;
        }

        // 3.1
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

        // 3.2
        public static double Cosx(double epsilon)
        {
            double x = Math.PI;

            while (Math.Abs(Math.Cos(x) + 1.0) > epsilon) 
            {
                x -= (Math.Cos(x) + 1.0) / Math.Sin(x); 
            }

            return x;
        }

        //3.3
        public static double Ex(double epsilon)
        {
            double x = 0.0;

            while (Math.Abs(Math.Exp(x) - 2.0) > epsilon) 
            {
                x -= (Math.Exp(x) - 2.0) / Math.Exp(x);
            }
            return x;
        }

        //3.4
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

        //3.5
        public static double ePowMinusOne(double epsilon)
        {
            return 0;
        }

        

        
        static void Main(string[] args)
        {
            double.TryParse(args[0], out double eps);
            // 3.1
            double x = Lnx(eps);
            if (x == double.NaN)
            {
                Console.WriteLine("wrong");
            }
            else
            {
                Console.WriteLine("Решение уравнения ln x = 1: {0}", x);
            }

            //3.2
            x = Cosx(eps);
            Console.WriteLine("Решение уравнения cos x = -1: {0}", x);
            

            //3.3
            x = Ex(eps);
            Console.WriteLine("Решение уравнения e^x = 2: {0}", x);

            //3.4
            x = X2(eps);
            Console.WriteLine("Решение уравнения x^2 = 2: {0}", x);

            //3.5
            x = ePowMinusOne(eps);
            Console.WriteLine("Решение последнего уравнения: {0}", x);

            //2.1
            x = Range1(eps);
            Console.WriteLine("Сумма ряда Σ1/n!: {0}", x);

            //2.2
            x = Range2(eps);
            Console.WriteLine("Сумма ряда pi=4*Σ((-1)^(n-1)/(2n-1): {0}", x);

            //2.3
            x = Range3(eps);
            Console.WriteLine("Сумма ряда ln2 = Σ((-1)^(n-1)/n: {0}", x);
            
            //2.4
            x = Range4(eps);
            Console.WriteLine("Сумма ряда sqrt(2) = П((2)^2)^-k): {0}", x);

            //2.5
            x = Range5(eps);
            Console.WriteLine("Сумма последнего ряда: {0}", x);
          
            //1.1
            x = Lim1(eps);
            Console.WriteLine("Первый предел: {0}", x);

            //1.2
            x = Lim2(eps);
            Console.WriteLine("Второй предел: {0}", x);
            
            ////1.3
            x = Lim3(eps);
            Console.WriteLine("Третий предел: {0}", x);
            
            //1.4
            x = Lim4(eps);
            Console.WriteLine("Четвертый предел: {0}", x);
            
            //1.5
            x = Lim5(eps);
            Console.WriteLine("Пятый предел: {0}", x);
         
        }
    }

}