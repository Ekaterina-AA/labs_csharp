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

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool fileCheck = false;
            string file_name = null;
            double sum = 0;
            int count = 0;

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
                        foreach (char c in line)
                        {
                            if (char.IsDigit(c) || char.IsLetter(c))
                            {
                                sum += (int)c;
                                count++;
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

            if (args.Length == 0 || args[0] == "-c" )
            {
                string input = Console.ReadLine();
                foreach (char c in input)
                {
                    sum += (int)c;
                    count++;
                }
            }

            if (count > 0)
            {
                double average = sum / count;
                Console.WriteLine("{0}", average);
            }
            else
            {
                Console.WriteLine("no digits were entered");
            }

        }
    }
}
