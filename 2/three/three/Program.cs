using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace three
{

public interface ICloneable
{
    object Clone();
}

    public sealed class Introvert: ICloneable
    {
        private int _value;

        public Introvert(
            int value)
        {
            _value = value;
        }

        public object Clone()
        {
            return new Introvert(_value);
        }
    }

    public class Person: IComparable, IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        private int SomeValue { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (obj is Person person)
            {
                return CompareTo(person);
            }

            throw new ArgumentException("Invalid object type", nameof(obj));
        }

        public int CompareTo(Person person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var comparisonResult = Age.CompareTo(person.Age);
            if (comparisonResult != 0)
            {
                return comparisonResult;
            }

            // TODO: name comparison
        }
    }

    public class AgeComparer : Comparer<Person>
    {
        public override int Compare(Person x, Person y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }

    public class ICompAgeComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public static class ArrayExtensions
    {
        public static void Sort<T>(this T[] array, SortOrder order, SortingAlgorithm algorithm)
            where T: IComparable<T>, new()
        {
            // Enum.IsDefined(order)
            // TODO: think about it
            array.Sort(order, algorithm, null);
        }

        public static void Sort<T>(this T[] array, SortOrder order, SortingAlgorithm algorithm, IComparer<T> comparer)
        {
            array.Sort(order, algorithm, comparer, null);


        }

        public static void Sort<T>(this T[] array, SortOrder order, SortingAlgorithm algorithm, Comparer<T> comparer)
        {
            array.Sort(order, algorithm, comparer as IComparer<T>);
        }

        /*private void Foo<T>(
            T[] values,
            IComparer<T> comparer
        )
        {
            comparer(values[3], values[6])
        }*/

        public static void Sort<T>(this T[] array, SortOrder order, SortingAlgorithm algorithm, Comparison<T> comparison)
        {
            switch (algorithm)
            {
                case SortingAlgorithm.Insertion:
                   
                case SortingAlgorithm.Selection:
                    
                case SortingAlgorithm.Heap:
                    
                case SortingAlgorithm.Quick:
                    
                case SortingAlgorithm.Merge:
                    
                default:
                    throw new ArgumentException("Invalid sorting algorithm");
            }
        }
    }

    public enum SortingAlgorithm
    {
        Insertion,
        Selection,
        Heap,
        Quick,
        Merge
    }

    class Program
    {
        static void Main()
        {
            Introvert i = new Introvert(10);
            Introvert i2 = i.Clone() as Introvert;
            
            var people = new List<Person>
            {
                new Person { Name = "Ivan", Age = 25 },
                new Person { Name = "John", Age = 30 },
                new Person { Name = "Maria", Age = 20 },
                new Person { Name = "Kate", Age = 40 },
                new Person { Name = "Vlad", Age = 44 }
            };

            var comparer1 = new AgeComparer();

            var comparer2 = new ICompAgeComparer();
            // Using default comparer for type int
            people.Sort(SortOrder.Ascending, SortingAlgorithm.Quick);
            Console.WriteLine("Sorted array in ascending order using Quick sort:");
            foreach (var item in people)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // Using custom comparer for type int
            people.Sort(SortOrder.Descending, SortingAlgorithm.Heap, comparer1);
            Console.WriteLine("Sorted array in descending order using Heap sort with custom comparer:");
            foreach (var item in people)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // Using custom icomparer for type int
            people.Sort(SortOrder.Descending, SortingAlgorithm.Heap, comparer2);
            Console.WriteLine("Sorted array in descending order using Heap sort with custom comparer:");
            foreach (var item in people)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            // Using comparison delegate for type int
            people.Sort(SortOrder.Ascending, SortingAlgorithm.Insertion, Person.CompareByName);
            Console.WriteLine("Sorted array in ascending order using Insertion sort with comparison delegate:");
            foreach (var item in people)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }

}
