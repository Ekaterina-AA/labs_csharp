using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace four
{
    using System;

    public interface IIntegralSolver
    {
        double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision);
        string SolverName { get; }
    }

    public class LeftRectangleSolver : IIntegralSolver
    {
        public double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision)
        {
            // implementation of left rectangle method
            int AmountOfSteps = 2;
            double integral = 0;
            double previousIntegral;

            do
            {
                previousIntegral = integral;
                double step = (upperBound - lowerBound) / AmountOfSteps;
                double HolderePlusStep = lowerBound;
                double SumOfFunctions = 0;

                for (int counter = 0; counter < AmountOfSteps; counter++)
                {
                    SumOfFunctions += function(HolderePlusStep);
                    HolderePlusStep += step;
                }

                integral = SumOfFunctions * step;
                AmountOfSteps += 1;
            } while (Math.Abs(integral - previousIntegral) > precision);
            
            return integral;
        }

        public string SolverName => "Left Rectangle Method";
    }

    public class RightRectangleSolver : IIntegralSolver
    {
        public double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision)
        {

            // implementation of right rectangle method
            int AmountOfSteps = 2;
            double integral = 0;
            double previousIntegral;

            do
            {
                previousIntegral = integral;
                double step = (upperBound - lowerBound) / AmountOfSteps;
                double HoldereMinusStep = upperBound;
                double SumOfFunctions = 0;

                for (int counter = AmountOfSteps; counter > 0; counter--)
                {
                    SumOfFunctions += function(HoldereMinusStep);
                    HoldereMinusStep -= step;
                }

                integral = SumOfFunctions * step;
                AmountOfSteps += 1;
            } while (Math.Abs(integral - previousIntegral) > precision);

            return integral;
        }
        public string SolverName => "Right Rectangle Method";
    }

    public class MiddleRectangleSolver : IIntegralSolver
    {
        public double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision)
        {
        // implementation of middle rectangle method
            int AmountOfSteps = 2;
            double integral = 0;
            double previousIntegral;

            do
            {
                previousIntegral = integral;
                double step = (upperBound - lowerBound) / AmountOfSteps;
                double HolderePlusStep = lowerBound;
                double SumOfFunctions = 0;

                for (int counter = 0; counter < AmountOfSteps; counter++)
                {
                    SumOfFunctions += function(HolderePlusStep + step / 2);
                    HolderePlusStep += step;
                }

                integral = SumOfFunctions * step;
                AmountOfSteps += 1;
            } while (Math.Abs(integral - previousIntegral) > precision);

            return integral;
        }

        public string SolverName => "Middle Rectangle Method";
    }

    public class TrapezoidSolver : IIntegralSolver
    {
        public double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision)
        {
            // implementation of trapezoid method
            int AmountOfSteps = 2;
            double integral = 0;
            double previousIntegral;

            do
            {
                previousIntegral = integral;
                double step = (upperBound - lowerBound) / AmountOfSteps;
                double HolderePlusStep = lowerBound;
                double SumOfFunctions = (function(HolderePlusStep) + function(upperBound - step)) / 2;
                HolderePlusStep = lowerBound + step + step;

                for (int counter = 0; counter < AmountOfSteps - 1; counter++)
                {
                    SumOfFunctions += function(HolderePlusStep);
                    HolderePlusStep += step;
                }

                integral = SumOfFunctions * step;
                AmountOfSteps += 1;
            } while (Math.Abs(integral - previousIntegral) > precision);

            return integral;
        }
        public string SolverName => "Trapezoid Method";
    }

    public class SimpsonSolver : IIntegralSolver
    {
        public double Solve(Func<double, double> function, double lowerBound, double upperBound, double precision)
        {
            // провверить все параметры
            // implementation of Simpson's method
            int AmountOfSteps = 2;
            double integral = 0;
            double previousIntegral;

            do
            {
                previousIntegral = integral;
                double step = (upperBound - lowerBound) / (AmountOfSteps);
                double HolderePlusStep = lowerBound;
                double SumOfFunctions = (function(HolderePlusStep) + function(upperBound));
                HolderePlusStep = lowerBound + step + step;

                for (int counter = 1; counter < AmountOfSteps + 1; counter++)
                {
                    if (counter % 2 == 0)
                    {
                        SumOfFunctions += function(HolderePlusStep) * 2;
                    }
                    else
                    {
                        SumOfFunctions += function(HolderePlusStep) * 4;
                    }
                    HolderePlusStep += step;
                }

                integral = SumOfFunctions * (step / 3);
                AmountOfSteps += 2;
            } while (Math.Abs(integral - previousIntegral) > precision);

            return integral;
        }

        public string SolverName => "Simpson's Method";
    }

    class Program
    {
        static void Main()
        {
            var methods = new IIntegralSolver[]
            {
                new LeftRectangleSolver(),
                new RightRectangleSolver(),
                new MiddleRectangleSolver(),
                new TrapezoidSolver(),
                new SimpsonSolver()
            };

            double function(double x)
            {
                return x * x + 2;
            }
            double lowerBound = 0;
            double upperBound = 1;
            double precision = 0.0001;

            foreach (var method in methods)
            {
                Console.WriteLine($"Using {method.SolverName}: {method.Solve(function, lowerBound, upperBound, precision)}");
            }
        }
    }
}
