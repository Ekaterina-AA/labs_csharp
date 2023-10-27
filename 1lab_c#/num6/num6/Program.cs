/*6.Реализуйте функцию, которая находит корень уравнения методом дихотомии. Аргументами
функции являются границы интервала, на котором находится корень, делегат, связанный с
уравнением, и точность, с которой корень необходимо найти. Продемонстрируйте работу
функции для различных границ интервала и точностей, для разных уравнений.*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number6
{
    // эписолон, а, б, наличие уравнения, 
    internal class Program
    {
        delegate double Equation(double x);

        public static double FirstEq(double x) => x * x - 2;
        public static double SecondEq(double x) => Math.Sin(x) - x / 2;
        public static double ThirdEq(double x) => Math.Exp(x) - 2;

        static double DihotomyMethod(double a, double b, Equation eq, double eps)
        {
            double c = (a + b) / 2;
            while (Math.Abs(b - a) > eps)
            {
                if (Math.Abs(eq(c)) < eps)
                {
                    return c;
                }
                else if (eq(a) * eq(c) < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                }
                c = (a + b) / 2;
            }
            return c;
        }

        static void Main(string[] args)
        {
            Equation a = new Equation(FirstEq);
            Equation b = new Equation(SecondEq);
            Equation c = new Equation(ThirdEq);

            // Решение уравнения x^2 - 2 = 0 на интервале [1, 2] с точностью 0.01
            double root1 = DihotomyMethod(1, 2, a, 0.01);
            Console.WriteLine("Root1: {0}", root1);

            // Решение уравнения sin(x) = x/2 на интервале [0, 1] с точностью 0.001
            double root2 = DihotomyMethod(0, 1, b, 0.001);
            Console.WriteLine("Root2: {0}", root2);

            // Решение уравнения e^x - 2 = 0 на интервале [-1, 1] с точностью 0.0001
            double root3 = DihotomyMethod(-1, 1, c, 0.0001);
            Console.WriteLine("Root3: {0}", root3);
        }
    }
}
