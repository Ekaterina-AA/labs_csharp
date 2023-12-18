using System;
using System.Collections.Generic;
using System.Linq;

namespace three
{
    public static class CombinationGenerator
    {
        static public IEnumerable<IEnumerable<T>> GenerateCombinationsWithRepetition<T>(this IEnumerable<T> input, int k, IEqualityComparer<T> comparer)
        {
            _ = input ?? throw new ArgumentNullException(nameof(input));
            _ = comparer ?? throw new ArgumentNullException(nameof(input));
            var set = new HashSet<T>(comparer);
            foreach (var item in input)
            {
                if (!set.Add(item))
                {
                    throw new ArgumentException("There should not be any duplicate elements");
                }
            }

            if (k < 0)
                throw new ArgumentException("k cannot be negative");

            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            foreach (var item in input)
            {
                var remaining = input.SkipWhile(e => !comparer.Equals(e, item));
                foreach (var combination in remaining.GenerateCombinationsWithRepetition(k - 1, comparer))
                {
                    //yield return new int[] { item }.Concat(combination);
                    yield return combination.Prepend(item);
                }
            }
        }

        static public IEnumerable<IEnumerable<int>> GenerateCombinationsWithoutRepetition(this IEnumerable<int> input, int k, IEqualityComparer<int> comparer)
        {
            var set = new HashSet<int>(comparer);
            foreach (var item in input)
            {
                if (!set.Add(item))
                {
                    throw new ArgumentException("There should not be any duplicate elements");
                }
            }

            if (k < 0)
                throw new ArgumentException("k cannot be negative");

            if (k == 0)
            {
                yield return Enumerable.Empty<int>();
                yield break;
            }

            foreach (var item in input)
            {
                var remaining = input.SkipWhile(e => comparer.Equals(e, item));
                foreach (var combination in remaining.GenerateCombinationsWithoutRepetition(k - 1, comparer))
                {
                    input = remaining;
                    yield return new int[] { item }.Concat(combination);
                }
            }
        }

        static public IEnumerable<IEnumerable<int>> GenerateSubsets(this IEnumerable<int> input, IEqualityComparer<int> comparer)
        {
            var set = new HashSet<int>(comparer);
            foreach (var item in input)
            {
                if (!set.Add(item))
                {
                    throw new ArgumentException("There should not be any duplicate elements");
                }
            }

            for (int counter = 0; counter < (1 << input.Count()); counter++)
            {
                yield return input.Where((t, j) => (counter & (1 << j)) != 0).ToArray();
            }
        }

        static public IEnumerable<IEnumerable<int>> GeneratePermutations(this IEnumerable<int> input, IEqualityComparer<int> comparer)
        {
            var set = new HashSet<int>(comparer);
            foreach (var item in input)
            {
                if (!set.Add(item))
                {
                    throw new ArgumentException("There should not be any duplicate elements");
                }
            }

            if (!input.Any())
                yield return Enumerable.Empty<int>();

            foreach (var item in input)
            {
                var remaining = input.Where(e => !comparer.Equals(e, item));
                foreach (var permutation in remaining.GeneratePermutations(comparer))
                {
                    yield return new int[] { item }.Concat(permutation);
                }
            }
        }
    }
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("one");
                int[] input1 = { 1, 2, 3 };
                foreach (var combination in input1.GenerateCombinationsWithRepetition(2, EqualityComparer<int>.Default))
                {
                    Console.WriteLine($"[{string.Join(", ", combination)}]");
                }

                Console.WriteLine("two");
                int[] input2 = { 1, 2, 3 };
                foreach (var combination in input2.GenerateCombinationsWithoutRepetition(2, EqualityComparer<int>.Default))
                {
                    Console.WriteLine($"[{string.Join(", ", combination)}]");
                }

                Console.WriteLine("three");
                int[] input3 = { 1, 2, 3 };
                foreach (var subset in input3.GenerateSubsets(EqualityComparer<int>.Default))
                {
                    Console.WriteLine($"[{string.Join(", ", subset)}]");
                }

                Console.WriteLine("four");
                int[] input4 = { 1, 2, 3 };
                foreach (var permutation in input4.GeneratePermutations(EqualityComparer<int>.Default))
                {
                    Console.WriteLine($"[{string.Join(", ", permutation)}]");
                }
            }
            catch (ArgumentException ex) when (ex.Message.Equals("There should not be any duplicate elements"))
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex) when (ex.Message.Equals("k cannot be negative"))
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}