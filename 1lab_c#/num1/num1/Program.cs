/*1. (10 баллов) Напишите две функции для поиска всех корней кубического уравнения двумя
разными способами: с помощью формулы Кордано и без неё. Результат вычислений возвращайте
как возвращаемое значение функции. Сравните полученные результаты*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace num1
{
    class Program
    {
        //
        private static void Gorner(double a, double b, double c, double d, ref double r1, ref double r2, ref double r3)
        {
            double temp = d * Math.Pow(a, 2);
            Console.WriteLine(temp);

            for (int i = 1; i <= (int)(temp); i++)
            {
                if (temp % i == 0)
                {
                    if (Math.Pow(i, 3) + b*Math.Pow(i, 2) + a*c*i + temp == 0)
                    {
                        r1 = (double)i / a;
                        break;
                    }
                    else if (Math.Pow(-i, 3) + b * Math.Pow(-i, 2) + a * c * (-i) + temp == 0)
                    {
                        r1 = (double)(-i) / a;
                        break;
                    }
                }
            }
           
            double a2 = a;
            double b2 = r1 * a2 + b;
            double c2 = r1 * b2 + c;

            var discriminant = Math.Pow(b2, 2) - 4 * a2 * c2;
            if (discriminant < 0)
            {
                r2 = double.NaN;
                r3 = double.NaN;
            }
            else
            {
                if (discriminant == 0)
                {
                    r2 = -b2 / (2 * a2);
                    r3 = r2;
                }
                else 
                {
                    r2 = (-b2 + Math.Sqrt(discriminant)) / (2 * a2);
                    r3 = (-b2 - Math.Sqrt(discriminant)) / (2 * a2);
                }
            }
        }

        //формула Кардано
        private static void Kardano(double a, double b, double c, double d, ref int type, ref double p1, ref double p2, ref double p3)
        {
            double eps = 1E-14;
            double p = (3 * a * c - b * b) / (3 * a * a);
            double q = (2 * b * b * b - 9 * a * b * c + 27 * a * a * d) / (27 * a * a * a);
            double det = q * q / 4 + p * p * p / 27;
            if (Math.Abs(det) < eps)
                det = 0;
            if (det > 0)
            {
                // один вещественный, два комплексных корня
                type = 1; 
                double u = -q / 2 + Math.Sqrt(det);
                u = Math.Exp(Math.Log(u) / 3);
                double yy = u - p / (3 * u);

                p1 = yy - b / (3 * a);
                p2 = -(u - p / (3 * u)) / 2 - b / (3 * a);
                p3 = Math.Sqrt(3) / 2 * (u + p / (3 * u));
            }
            else
            {
                if (det < 0)
                {
                    // три вещественных корня
                    type = 2; 
                    double fi;
                    if (Math.Abs(q) < eps) 
                        fi = Math.PI / 2;
                    else
                    {
                        if (q < 0)
                            fi = Math.Atan(Math.Sqrt(-det) / (-q / 2));
                        else 
                            fi = Math.Atan(Math.Sqrt(-det) / (-q / 2)) + Math.PI;
                    }
                    double r = 2 * Math.Sqrt(-p / 3);
                    p1 = r * Math.Cos(fi / 3) - b / (3 * a);
                    p2 = r * Math.Cos((fi + 2 * Math.PI) / 3) - b / (3 * a);
                    p3 = r * Math.Cos((fi + 4 * Math.PI) / 3) - b / (3 * a);
                }

                else if (det == 0)
                {
                    if (Math.Abs(q) < eps)
                    {
                        // 3-х кратный 
                        type = 4; 
                        p1 = -b / (3 * a); 
                        p2 = -b / (3 * a);
                        p3 = -b / (3 * a);
                    }
                    else
                    {
                        // один и два кратных
                        type = 3; 
                        double u = Math.Exp(Math.Log(Math.Abs(q) / 2) / 3);
                        if (q < 0)
                            u = -u;
                        p1 = -2 * u - b / (3 * a);
                        p2 = u - b / (3 * a);
                        p3 = u - b / (3 * a);
                    }
                }
            }
        } 

        static void Main(string[] args)
        {
            // 1, 3, 4, 2        тип 1
            // 1, 6, 3, -10      тип 2
            // 1, 12, 36, 32     тип 3
            // 3, -9, 9, -3      тип 4

            double a = 1.0;
            double b = 3.0;
            double c = 3.0;
            double d = -1.0;
            int type = 0;
            double p1 = 0, p2 = 0, p3 = 0;
            double r1 = 0, r2 = 0, r3 = 0;

            Kardano(a, b, c, d, ref type, ref p1, ref p2, ref p3);
            if (type == 1)
                Console.WriteLine("Один вещественный и два комплексно сопряженных корня: root1={0} root2/3:{1} +- {2}i", p1, p2, p3);
            else if (type == 2)
                Console.WriteLine("3 действительных корня: root1={0} root2={1} root3={2}", p1, p2, p3);
            else if (type == 3)
                Console.WriteLine("3 вещественных корня, два из которых кратные: root1={0} root2/3={1}", p1, p2);
            else if (type == 4)
                Console.WriteLine("3 кратных действительных корня: root1/2/3:{0}", p1);

            Gorner(a, b, c, d, ref r1, ref r2, ref r3);
            Console.WriteLine("Корни: root1={0}  root2={1}   root3={2}", r1, r2, r3);
        }
        
    }
}