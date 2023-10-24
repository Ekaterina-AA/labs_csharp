/*
3. a) Реализуйте приложение для обработки последовательности символов, разделенных
пробелами. Необходимо вычислить их среднее арифметическое (по значениям кодов символов).
Примечание.Ввод символа «не цифры» является корректным. Ввод данных осуществлять в
зависимости от флагов, которые передаются программе как аргументы командной строки:
-c или отсутствие флага означает считывание данных с консоли в интерактивном режиме;
-f означает что символы нужно считать из файла, путь к которому передаётся как аргумент
командной строки.
b) (5 баллов) Реализуйте приложение для обработки числовых данных. Из файла или с консоли
необходимо прочитать произвольное (заранее неизвестное) число чисел и найти их среднее
геометрическое, среднее гармоническое. При вводе недопустимого символа вывести
информацию об ошибке. Примечание. Допускается ввод целых и вещественных чисел.
Разделителем целой и дробной части является символ точки или запятой. Разделителем между
числами являются символы пробела, табуляции, переноса строки, в произвольном количестве.
Интерфейс приложения реализовать так же как и в задаче а).*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool fileCheck = false;
            string file_name = null;
            double geometric = 1;
            double harmonic = 0;
            int count = 0;
            double number;

            if (args.Length != 0 && args[0] == "-f" && 1 < args.Length)
            {
                fileCheck = true;
                file_name = args[1];
            }

            if (fileCheck)
            {
                try
                {
                    StreamReader file_open = new StreamReader(file_name);
                    while (!file_open.EndOfStream)
                    {
                        string line = file_open.ReadLine();
                        string[] token = line.Split(' ', '\n', '\t', '\r');
                        foreach (string token2 in token)
                        {
                            if (double.TryParse(token2, out number))
                            {
                                if (number <= 0)
                                {
                                    Console.WriteLine("Invalid input: {0}", number);
                                }
                                else
                                {
                                    geometric *= number;
                                    harmonic *= 1.0 / number;
                                    count++;
                                }

                            }
                        }
                    }
                    file_open.Close();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            if ( args.Length == 0 || args[0] == "-c")
            {
                Console.WriteLine("string: ");
                string input = Console.ReadLine();
                string[] token = input.Split(' ', '\n', '\t', '\r');
                foreach (string token2 in token)
                {
                    if (double.TryParse(token2, out number))
                    {
                        if (number <= 0)
                        {
                            Console.WriteLine("Invalid input: {0}", number);
                        }
                        else
                        {
                            geometric *= number;
                            harmonic += 1.0 / number;
                            count++;
                        }
                    }
                }
            }

            if (count > 0)
            {
                double average_geometric = Math.Pow(geometric, 1.0 / count);
                double average_harmonic = count / harmonic;
                Console.WriteLine("Average geometric: {0} ", average_geometric);
                Console.WriteLine("Average harmonic: {0}", average_harmonic);
            }
            else
            {
                Console.WriteLine("no digits were entered");
            }

        }
    }
}