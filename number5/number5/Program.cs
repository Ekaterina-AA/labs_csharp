/*5.Пользователем задается строка из файла или с клавиатуры. При взаимодействии с программой
через интерактивный диалог, в зависимости от выбора пользователя необходимо:
a) Выполнить сортировку слов строки по алфавиту и вывести на экран слово, состоящее из
последних символов этих слов.
b) В каждом слове строки поднять регистр первой буквы слова и опустить регистр последней
буквы.
c) Подсчитать, сколько раз в этой строке встречается заданное (ввод с консоли) слово.
d) Заменить в данной строке предпоследнее слово на слово, которое ввел пользователь (ввод с
консоли).
e) Найти 𝑘 − ое слово в строке, начинающееся с заглавной буквы (ввод с консоли).
Результаты вычислений необходимо вывести на консоль.*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace number5
{
    internal class Program
    {
        /* a) Выполнить сортировку слов строки по алфавиту и вывести на экран слово, состоящее из
           последних символов этих слов.*/
        static void Atask(string line)
        {
           string[] words = line.Split(' ');
           Array.Sort(words);
           string lastChars = "";
           foreach (string word in words)
           {
               lastChars += word.Substring(word.Length - 1);
           }
           Console.WriteLine(lastChars);
        }

        /*b) В каждом слове строки поднять регистр первой буквы слова и опустить регистр последней буквы.*/
        static void Btask(string line)
        {
            string[] words = line.Split(' ');
            string result = "";
            foreach (string word in words)
            {
                string firstChar = word.Substring(0, 1);
                string lastChar = word.Substring(word.Length - 1);
                string middleChars = word.Substring(1, word.Length - 2);
                result += firstChar.ToUpper() + middleChars + lastChar.ToLower() + " ";
            }
            Console.WriteLine(result);

        }

        /*c) Подсчитать, сколько раз в этой строке встречается заданное(ввод с консоли) слово.*/
        static void Ctask(string line, string word_for_search)
        {
            int count = 0;
            string[] words = line.Split(' ');
            foreach(string word in words)
            {
                if (word == word_for_search)
                {
                    count++;
                }
            }
            Console.WriteLine(count);
        }

        /* d) Заменить в данной строке предпоследнее слово на слово, которое ввел пользователь(ввод с консоли).*/
        static void Dtask(string line, string word_for_search)
        {
            string result = "";
            string[] words = line.Split(' ');
            words[words.Length - 2] = word_for_search;
            foreach(string word in words)
            {
                result += word;
                result += " ";
            }
            Console.WriteLine(result);
        }

        /* e) Найти 𝑘 − ое слово в строке, начинающееся с заглавной буквы(ввод с консоли). Результаты вычислений необходимо вывести на консоль.*/
        static void Etask(string line, int number)
        {
            int count = 0;
            string[] words = line.Split(' ');
            foreach (string word in words)
            {
                if (char.IsUpper(word[0]))
                {
                    count++;
                }

                if (count == number)
                {
                    Console.WriteLine(word);
                    break;
                }
            }
            
        }



        static string File_check(string file_name)
        {
            StreamReader file = new StreamReader(file_name);
            if (file != null)
            {
                return file.ReadLine();
            }
            else
            {
                Console.WriteLine("file cannot be opened");
            }
            return null;
        }


        static void Main(string[] args)
        {
            Console.WriteLine("file or console: ");
            string file_or_console = Console.ReadLine();
            Console.WriteLine("a, b, c, d or e: ");
            string c = Console.ReadLine();
            string line;


           /* a) Выполнить сортировку слов строки по алфавиту и вывести на экран слово, состоящее из
           последних символов этих слов.*/
            if (c == "a")
            {
                if (file_or_console == "file")
                {
                    string filename = "";
                    line = File_check(filename);
                    if (line != null)
                    {
                        Atask(line);
                    }
                    
                }
                else
                {
                    line = Console.ReadLine();
                    if (line != null)
                    {
                        Atask(line);
                    }
                }
            }

            /*b) В каждом слове строки поднять регистр первой буквы слова и опустить регистр последней буквы.*/
            if (c == "b")
            {
                if (file_or_console == "file")
                {
                    string filename = "";
                    line = File_check(filename);
                    if (line != null)
                    {
                        Btask(line);
                    }

                }
                else
                {
                    line = Console.ReadLine();
                    if (line != null)
                    {
                        Btask(line);
                    }
                }
            }

            /*c) Подсчитать, сколько раз в этой строке встречается заданное(ввод с консоли) слово.*/
            if (c == "c")
            {
                Console.WriteLine("word: ");
                string word_for_search = Console.ReadLine();
                if (file_or_console == "file")
                {
                    string filename = "";
                    line = File_check(filename);
                    if (line != null)
                    {
                        Ctask(line, word_for_search);
                    }

                }
                else
                {
                    line = Console.ReadLine();
                    if (line != null)
                    {
                        Ctask(line, word_for_search);
                    }
                }
            }
            /* d) Заменить в данной строке предпоследнее слово на слово, которое ввел пользователь(ввод с консоли).*/
            if (c == "d")
            {
                Console.WriteLine("word: ");
                string word_for_search = Console.ReadLine();

                if (file_or_console == "file")
                {
                    string filename = "";
                    line = File_check(filename);
                    if (line != null)
                    {
                        Dtask(line, word_for_search);
                    }

                }
                else
                {
                    line = Console.ReadLine();
                    if (line != null)
                    {
                        Dtask(line, word_for_search);
                    }
                }
            }
            /* e) Найти 𝑘 − ое слово в строке, начинающееся с заглавной буквы(ввод с консоли). Результаты вычислений необходимо вывести на консоль.*/
            if (c == "e")
            {
                Console.WriteLine("number: ");
                string k_for_search = Console.ReadLine();
                int.TryParse(k_for_search, out int k); 

                if (file_or_console == "file")
                {
                    string filename = "";
                    line = File_check(filename);
                    if (line != null)
                    {
                        Etask(line, k);
                    }
                }
                else
                {
                    line = Console.ReadLine();
                    if (line != null)
                    {
                        Etask(line, k);
                    }
                }
            }
        }
    }
}
