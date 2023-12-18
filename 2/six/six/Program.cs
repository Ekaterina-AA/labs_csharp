using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

public interface IPrimalityTest
{
    /*enum PrimalityTestResult
    {
        Prime,
        Composite,
        NeitherPrimeTorComposite
    };*/
    bool IsPrime(int number, double minProbability);
}
public abstract class ProbabilisticPrimalityTest : HelperFunctions, IPrimalityTest
{
    public int CalculateIterations(double minProbability)
    {
        if (minProbability < 0 || minProbability >= 1)
        {
            throw new ArgumentException("Invalid epsilon value", nameof(minProbability));
        }

        var iterationsCount = 0;
        double accumulator = 1;
        do
        {
            iterationsCount++;
            accumulator *= OneIterationCompositanceProbability;
        } while (accumulator >= 1 - minProbability);

        return iterationsCount;
    }

    protected abstract double OneIterationCompositanceProbability
    {
        get;
    }

    public bool IsPrime(int number, double minProbability)
    {
        if (number <= 0)
        {
            throw new ArgumentException("Value can't be lower or equal to 0", nameof(number));
        }
        if (number == 1)
        {
            throw new ArgumentException("Value 1 is not prime nor composite", nameof(number));
        }
        if (number <= 3)
        {
            return true;
        }
        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }
        var iterationsCount = CalculateIterations(minProbability);
        Random random = new Random();
        for (var counter = 0; counter < iterationsCount; counter++)
        {
            int a = random.Next(2, number - 2);
            if (!TestIteration(number, a))
            {
                return false;
            }
        }
        return true;
    }

    protected abstract bool TestIteration(int primeCandidate, int iterationParameter);
}



public class DeterministicPrimalityTest : IPrimalityTest
{
    public bool IsPrime(int number, double minProbability)
    {
        if (number <= 0)
        {
            throw new ArgumentException("Value can't be lower or equal to 0", nameof(number));
        }
        if (number == 1)
        {
            throw new ArgumentException("Value 1 is not prime nor composite", nameof(number));
        }
        if (number <= 3)
        {
            return true;
        }
        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        for (int counter = 5; counter * counter <= number; counter += 2)
        {
            if (number % counter == 0)
            {
                return false;
            }
        }
        return true;
    }
}

public class FermatPrimalityTest : ProbabilisticPrimalityTest
{
    protected override double OneIterationCompositanceProbability =>
        0.5;
    protected override bool TestIteration(int primeCandidate, int iterationParameter)
    {
        if (GCD(primeCandidate, iterationParameter) != 1)
        {
            return false;
        }
        if (ModPow(iterationParameter, primeCandidate - 1, primeCandidate) != 1)
        {
            return false;
        }
        return true;
    }
}

public class SolovayStrassenPrimalityTest : ProbabilisticPrimalityTest
{
    protected override double OneIterationCompositanceProbability =>
        0.5;

    protected override bool TestIteration(int primeCandidate, int iterationParameter)
    {
        if (GCD(iterationParameter, primeCandidate) > 1)
        {
            return false;
        }

        int jacobi = JacobiSymbol(iterationParameter, primeCandidate);
        int mod = ModPow(iterationParameter, (primeCandidate - 1) / 2, primeCandidate);

        if (jacobi == 0)
        {
            return true;
        }
        if (mod == primeCandidate - 1)
        {
            return true;
        }
        if (mod == jacobi)
        {
            return true;
        }
        if (mod == 1 || mod == 0)
        {
            return true;
        }
        return false;
    }
}

public class MillerRabinPrimalityTest : ProbabilisticPrimalityTest
{
    protected override double OneIterationCompositanceProbability =>
        0.25;
    protected override bool TestIteration(int primeCandidate, int iterationParameter)
    {
        int d = primeCandidate - 1;
        int s = 0;
        while (d % 2 == 0)
        {
            d /= 2;
            s++;
        }

        int jacobi = JacobiSymbol(iterationParameter, primeCandidate);
        int mod = ModPow(iterationParameter, d, primeCandidate);

        if (jacobi == 0)
        {
            return true;
        }
        if (mod == primeCandidate - 1)
        {
            return true;
        }
        if (mod == jacobi)
        {
            return true;
        }
        if (mod == 1 || mod == 0)
        {
            return true;
        }

        for (int counter = 1; counter < s; counter++)
        {
            int exponent = (int)Math.Pow(2, counter);
            int power = ModPow(iterationParameter, d * exponent, primeCandidate);
            if (jacobi == 0)
            {
                return true;
            }
            if (power == primeCandidate - 1)
            {
                return true;
            }
            if (power == jacobi)
            {
                return true;
            }
            if (power == 1)
            {
                return true;
            }
        }

        return false;
    }
}

public class HelperFunctions
{
    protected int GCD(int iterationParameter, int primeCandidate)
    {
        int k = 1;
        while ((iterationParameter != 0) && (primeCandidate != 0))
        {
            while (((iterationParameter & 1) == 0) && ((primeCandidate & 1) == 0))
            {
                iterationParameter >>= 1;
                primeCandidate >>= 1;
                k <<= 1;
            }
            while ((iterationParameter & 1) == 0)
            {
                iterationParameter >>= 1;
            }
            while (((primeCandidate & 1) == 0))
            {
                primeCandidate >>= 1;
            }
            if (iterationParameter >= primeCandidate)
            {
                iterationParameter -= primeCandidate;
            }
            else
            {
                primeCandidate -= iterationParameter;
            }
        }
        return primeCandidate * k;
    }


    protected int JacobiSymbol(int iterationParameter, int primeCandidate)
    {
        int r = 1;
        while (iterationParameter != 0)
        {
            int t = 0;
            while ((iterationParameter & 1) == 0)
            {
                t++;
                iterationParameter >>= 1;
            }

            if ((t & 1) != 0)
            {
                int temp = primeCandidate % 6;
                if (temp == 3 || temp == 5)
                {
                    r = -r;
                }
            }
            int c = iterationParameter;
            iterationParameter = primeCandidate % c;
            primeCandidate = c;
        }
        return r;
    }

    protected int ModPow(int iterationParameter, int PowerOfA, int mod)
    {
        if (PowerOfA == 0)
            return 1;
        if (PowerOfA % 2 == 1)
            return (iterationParameter * ModPow(iterationParameter, PowerOfA - 1, mod)) % mod;
        else
        {
            int halfResult = ModPow(iterationParameter, PowerOfA / 2, mod);
            return (halfResult * halfResult) % mod;
        }
    }
}


class Program
{
    static void Main()
    {
        try
        {
            int value = 10301;
            IPrimalityTest test = new SolovayStrassenPrimalityTest();
            bool isPrime = test.IsPrime(value, 0.99);
            Console.WriteLine($"{value} is probably prime: {isPrime}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}